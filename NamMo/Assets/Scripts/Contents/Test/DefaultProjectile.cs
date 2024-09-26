using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : BaseProjectile, IParryingable
{
    private Vector2 _direction;
    public override void SetAttackInfo(GameObject attacker, float damage, float speed = 0, GameObject target = null)
    {
        base.SetAttackInfo(attacker, damage, speed, target);
        _target = target;
        if (_target)
        {
            _direction = (target.transform.position - transform.position).normalized;
        }
    }

    protected override void UpdateAttack()
    {
        gameObject.transform.position += new Vector3(_direction.x, _direction.y) * _speed * Time.deltaTime;
    }

    public void Parried(GameObject attacker, GameObject target = null)
    {
        SetAttackInfo(attacker, _damage, _speed, target);
        _direction *= -1;
    }
}
