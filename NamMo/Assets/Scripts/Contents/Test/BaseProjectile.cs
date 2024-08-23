using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileInfo
{
    public GameObject target;
    public float speed;
    public float damage; 
    public GameObject owner;
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected LayerMask _destroyLayer;
    protected ProjectileInfo _projectileInfo = new ProjectileInfo();
    private bool _blockCheck = false;
    public GameObject Target { get { return _projectileInfo.target; } }
    public float Speed { get { return _projectileInfo.speed; } }
    public float Damage { get { return _projectileInfo.damage; } }
    public GameObject Owner { get { return _projectileInfo.owner; } }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        UpdateProjectile();
    }
    private void FixedUpdate()
    {
        FixedUpdateProjectile();
    }
    protected virtual void Init()
    {
        _collider = GetComponent<Collider2D>();
    }
    protected virtual void UpdateProjectile() { }
    protected virtual void FixedUpdateProjectile() { }
    public virtual void SetProjectileInfo(GameObject target, float speed, float damage, GameObject owner)
    {
        _projectileInfo.target = target;
        _projectileInfo.speed = speed;
        _projectileInfo.damage = damage;
        _projectileInfo.owner = owner;
    }
    public abstract void Parried();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 파괴되는 layer에 걸리면 파괴
        if(((1 << collision.gameObject.layer) & _destroyLayer.value) != 0)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }
        // 발사체의 주인과 충돌한 물체의 주인이 같다면 return
        if (collision.gameObject == _projectileInfo.owner) return;
        if (_projectileInfo.owner != null)
        {
            if (_projectileInfo.owner.GetComponent<PlayerController>())
            {
                // 주인이 Player일 때 충돌한게 BlockArea라면 return
                if (collision.gameObject.GetComponent<BlockArea>()) return;
            }
        }
        // 타겟이 지정되어있지 않다면 Player, Enemy 둘 다 체크
        if (_projectileInfo.target == null)
        {
            CheckPlayer(collision); 
            CheckEnemy(collision);
        }
        else
        {
            // 타겟이 플레이어면 플레이어만 체크
            if (_projectileInfo.target.GetComponent<PlayerController>())
            {
                CheckPlayer(collision);
            }
            else //TODO : Enemy Check
            {
                CheckEnemy(collision);
            }
        }
    }
    private void CheckPlayer(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BlockArea ba = collision.gameObject.GetComponent<BlockArea>();
        if (pc == null && ba == null) return;
        if (ba)
        {
            if (_blockCheck == false)
            {
                _blockCheck = true;
                ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
            }
        }
        if (pc)
        {
            if (_blockCheck == false)
            {
                List<Collider2D> results = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D().NoFilter();
                GetComponent<Collider2D>().OverlapCollider(filter, results);
                foreach (Collider2D c in results)
                {
                    BlockArea blockArea = c.gameObject.GetComponent<BlockArea>();
                    if (blockArea)
                    {
                        _blockCheck = true;
                        blockArea.OnBlockAreaTriggerEntered.Invoke(gameObject);
                        break;
                    }
                }
            }
            bool damaged = pc.GetPlayerCombatComponent().GetDamaged(_projectileInfo.damage, transform.position);
            if (damaged)
            {
                Managers.Resource.Destroy(gameObject);
            }
        }
        if (_blockCheck)
        {
            StartCoroutine(CoRefreshBlockCheck());
        }
    }
    private void CheckEnemy(Collider2D collision)
    {
        // TODO
    }
    IEnumerator CoRefreshBlockCheck()
    {
        yield return new WaitForEndOfFrame();
        _blockCheck = false;
    }
}
