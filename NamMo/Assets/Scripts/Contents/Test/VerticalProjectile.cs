using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalProjectile : BaseProjectile
{
    protected override void Init()
    {
        base.Init();
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }
    public override void Parried()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
