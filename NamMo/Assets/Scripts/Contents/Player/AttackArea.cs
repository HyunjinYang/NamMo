using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private BoxCollider2D _collider;
    public Action<GameObject> OnAttackAreaTriggerEntered;
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DummyEnemy>() == null) return;
        if (OnAttackAreaTriggerEntered != null) OnAttackAreaTriggerEntered.Invoke(collision.gameObject);
    }
    public void SetAttackRange(Vector2 range, Vector2 offset)
    {
        _collider.size = range;
        _collider.offset = offset;
    }
    public void ActiveAttackArea()
    {
        _collider.enabled = true;
    }
    public void DeActiveAttackArea()
    {
        _collider.enabled = false;
    }
}
