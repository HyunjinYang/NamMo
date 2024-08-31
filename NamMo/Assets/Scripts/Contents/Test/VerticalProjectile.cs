using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalProjectile : BaseProjectile, IParryingable
{
    protected override void Init()
    {
        base.Init();
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }
    public void Parried(GameObject attacker, GameObject target = null)
    {
        Managers.Resource.Destroy(gameObject);
    }
}
