using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BaseProjectile : BaseAttack
{
    [SerializeField] protected LayerMask _destroyLayer;

    protected float _speed;
    protected GameObject _target;

    private bool _blockedCurrentFrame = false;
    public float Speed { get { return _speed; } }
    public override void SetAttackInfo(GameObject attacker, float damage, int attackStrength = 1, float speed = 0, GameObject target = null)
    {
        base.SetAttackInfo(attacker, damage, attackStrength);
        _speed = speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }
    protected virtual void CheckCollision(Collider2D collision)
    {
        // 파괴되는 layer에 걸리면 파괴
        if (((1 << collision.gameObject.layer) & _destroyLayer.value) != 0)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }
        if (collision.gameObject == _attacker) return;
        
        if (_attackerType == AttackerType.Player)
        {
            TryHit(collision.gameObject);
        }
        else
        {
            CheckPlayerBlock(collision);
        }
    }
    protected void CheckPlayerBlock(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BlockArea ba = collision.gameObject.GetComponent<BlockArea>();
        if (pc == null && ba == null) return;
        if (ba)
        {
            if (_blockedCurrentFrame == false)
            {
                _blockedCurrentFrame = true;
                ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
            }
        }
        if (pc)
        {
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
                        Debug.Log("실햄");
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
    // Coroutine
    IEnumerator CoRefreshBlockCheck()
    {
        yield return new WaitForEndOfFrame();
        _blockedCurrentFrame = false;
    }
}
