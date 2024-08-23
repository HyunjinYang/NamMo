using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : BaseProjectile
{
    private Vector2 _direction;
    public override void SetProjectileInfo(GameObject target, float speed, float damage, GameObject owner)
    {
        base.SetProjectileInfo(target, speed, damage, owner);
        if (target)
        {
            _direction = (target.transform.position - transform.position).normalized;
        }
    }
    protected override void UpdateProjectile()
    {
        _direction = Vector2.Lerp(_direction, (_projectileInfo.target.transform.position + (new Vector3(_direction.x, _direction.y)) * 2 - transform.position).normalized, 0.05f);
        gameObject.transform.position += new Vector3(_direction.x, _direction.y) * _projectileInfo.speed * Time.deltaTime;
    }
    public override void Parried()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
