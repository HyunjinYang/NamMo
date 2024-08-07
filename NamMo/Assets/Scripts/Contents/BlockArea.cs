using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArea : MonoBehaviour
{
    private Collider2D _collider;
    public Action<GameObject> OnBlockAreaTriggerEntered;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAttack>() == null) return;
        if (OnBlockAreaTriggerEntered != null) OnBlockAreaTriggerEntered.Invoke(collision.gameObject);
    }
    public void ActiveBlockArea()
    {
        _collider.enabled = true;
    }
    public void DeActiveBlockArea()
    {
        _collider.enabled = false;
    }
}
