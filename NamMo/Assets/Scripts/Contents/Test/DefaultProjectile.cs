using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : BaseProjectile
{
    private Vector2 _direction;
    public override void SetProjectileInfo(GameObject target, float speed, float damage, GameObject owner = null)
    {
        base.SetProjectileInfo(target, speed, damage, owner);
        if (target)
        {
            _direction = (target.transform.position - transform.position).normalized;
        }
    }

    protected override void UpdateProjectile()
    {
        gameObject.transform.position += new Vector3(_direction.x, _direction.y) * _projectileInfo.speed * Time.deltaTime;
    }
    public override void Parried()
    {
        _direction *= -1;
    }
}
