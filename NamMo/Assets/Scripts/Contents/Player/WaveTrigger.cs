using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    private CircleCollider2D _circleCollider;
    public Action<GameObject> OnWaveRangeTriggerEntered = null;
    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    public void SetRadius(float value)
    {
        _circleCollider.radius = value;
    }
    public float GetRadius()
    {
        return _circleCollider.radius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO : 적 파동기 탐지
        if (OnWaveRangeTriggerEntered != null) OnWaveRangeTriggerEntered.Invoke(collision.gameObject);
    }
}
