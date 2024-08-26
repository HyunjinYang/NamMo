using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseAttack : MonoBehaviour
{
    private bool _blockedCurrentFrame = false;
    protected Collider2D _collider;
    protected GameObject _attacker;
    protected float _damage;

    public Action<GameObject> OnHitted;
    public GameObject Attakcer { get { return _attacker; } }
    public float Damage { get { return _damage; } }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        UpdateAttack();
    }
    private void FixedUpdate()
    {
        FixedUpdateAttack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }
    private void OnDestroy()
    {
        OnHitted = null;
    }
    // public
    public virtual void SetAttackInfo(GameObject attacker, float damage, float speed = 0, GameObject target = null)
    {
        _attacker = attacker;
        _damage = damage;
    }
    public void ActiveAttackArea()
    {
        _collider.enabled = true;
    }
    public void DeActiveAttackArea()
    {
        _collider.enabled = false;
    }
    // protected
    protected virtual void Init()
    {
        _collider = GetComponent<Collider2D>();
    }
    protected virtual void UpdateAttack() { }
    protected virtual void FixedUpdateAttack() { }
    protected virtual void CheckCollision(Collider2D collision)
    {
        // 공격자와 충돌한 오브젝트가 같다면 return
        if (collision.gameObject == _attacker) return;
        if (_attacker.GetComponent<PlayerController>())
        {
            CheckEnemy(collision);
        }
        else
        {
            CheckPlayer(collision);
        }
    }
    // private
    private void CheckPlayer(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BlockArea ba = collision.gameObject.GetComponent<BlockArea>();
        if (pc == null && ba == null) return;
        if (ba)
        {
            // 플레이어의 방어 영역에 닿았다면 GA_Block쪽 코드 실행
            if (_blockedCurrentFrame == false)
            {
                _blockedCurrentFrame = true;
                ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
            }
        }
        if (pc)
        {
            // 플레이어의 영역에 닿았다면 방어 영역도 닿아있는지 체크, 닿아있다면 GA_Block쪽 코드 실행
            // 방어(패링)를 우선으로 체크해야 하는데 한 프레임에 겹쳐있을 경우 어떤 판정이 먼저 들어올지 모르기 때문
            if (_blockedCurrentFrame == false)
            {
                List<Collider2D> results = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D().NoFilter();
                GetComponent<Collider2D>().OverlapCollider(filter, results);
                foreach (Collider2D c in results)
                {
                    BlockArea blockArea = c.gameObject.GetComponent<BlockArea>();
                    if (blockArea)
                    {
                        _blockedCurrentFrame = true;
                        blockArea.OnBlockAreaTriggerEntered.Invoke(gameObject);
                        break;
                    }
                }
            }
            TryHit(pc.gameObject);
        }
        if (_blockedCurrentFrame)
        {
            StartCoroutine(CoRefreshBlockCheck());
        }
    }
    private void CheckEnemy(Collider2D collision)
    {
        Enemy.Enemy enemy = collision.gameObject.GetComponent<Enemy.Enemy>();
        DummyEnemy dummyEnemy = collision.gameObject.GetComponent<DummyEnemy>();
        if (enemy == null && dummyEnemy == null) return;
        TryHit(collision.gameObject);
    }
    private void TryHit(GameObject target)
    {
        if (target.GetComponent<PlayerController>())
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc.GetASC().IsExsistTag(Define.GameplayTag.Player_State_Invincible)) return;
            pc.GetPlayerCombatComponent().GetDamaged(_damage, transform.position);
        }
        else if (target.GetComponent<Enemy.Enemy>())
        {
            Enemy.Enemy enemy = target.GetComponent<Enemy.Enemy>();
            enemy.Hit((int)_damage);
        }
        // tmp
        else if(target.GetComponent<DummyEnemy>()) 
        {
            target.GetComponent<DummyEnemy>().Damaged(_damage);
        }
        if (OnHitted != null) OnHitted.Invoke(target);
    }
    // Coroutine
    IEnumerator CoRefreshBlockCheck()
    {
        yield return new WaitForEndOfFrame();
        _blockedCurrentFrame = false;
    }
}
