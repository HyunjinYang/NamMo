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
    public float Speed { get { return _speed; } }
    public override void SetAttackInfo(GameObject attacker, float damage, float speed = 0, GameObject target = null)
    {
        base.SetAttackInfo(attacker, damage);
        _speed = speed;
    }

    protected override void CheckCollision(Collider2D collision)
    {
        // 파괴되는 layer에 걸리면 파괴
        if (((1 << collision.gameObject.layer) & _destroyLayer.value) != 0)
        {
            Managers.Resource.Destroy(gameObject);
            return;
        }
        base.CheckCollision(collision);
    }
}
