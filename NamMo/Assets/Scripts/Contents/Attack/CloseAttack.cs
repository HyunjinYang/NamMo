using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CloseAttack : BaseAttack
{
    private BoxCollider2D _boxCollider;
    protected override void Init()
    {
        base.Init();
        _boxCollider = _collider as BoxCollider2D;
        _boxCollider.enabled = false;
    }
    public void SetAttackRange(Vector2 range, Vector2 offset)
    {
        _boxCollider.size = range;
        _boxCollider.offset = offset;
    }
}
