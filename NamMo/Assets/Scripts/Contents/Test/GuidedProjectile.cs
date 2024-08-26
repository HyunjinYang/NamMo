using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : BaseProjectile, IParryingable
{
    private Vector2 _direction;
    public override void SetAttackInfo(GameObject attacker, float damage, float speed = 0, GameObject target = null)
    {
        base.SetAttackInfo(attacker, damage, speed, target);
        _target = target;
        _direction = (target.transform.position - transform.position).normalized;
    }
    protected override void UpdateAttack()
    {
        _direction = Vector2.Lerp(_direction, (_target.transform.position + (new Vector3(_direction.x, _direction.y)) * 2 - transform.position).normalized, 0.05f);
        gameObject.transform.position += new Vector3(_direction.x, _direction.y) * _speed * Time.deltaTime;
    }
    public void Parried(GameObject attacker, GameObject target = null)
    {
        Managers.Resource.Destroy(gameObject);
    }
}
