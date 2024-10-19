using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : BaseProjectile, IParryingable
{
    private Vector2 _direction;
    public override void SetAttackInfo(GameObject attacker, float damage, int attackStrength, float speed = 0, GameObject target = null)
    {
        base.SetAttackInfo(attacker, damage, attackStrength, speed, target);
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
        SetAttackInfo(attacker, _damage, _attackStrength, _speed, target);
        _direction *= -1;
    }
}
