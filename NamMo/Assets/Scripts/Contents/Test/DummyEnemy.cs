using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    private Coroutine _attackedEffectCoroutine = null;
    [SerializeField] private SpriteRenderer _spriteRenderer_Inside;
    [SerializeField] private SpriteRenderer _spriteRenderer_Outside;
    public void Damaged(float damage)
    {
        Debug.Log($"Damaged : {damage}");
        if (_attackedEffectCoroutine != null)
        {
            StopCoroutine(_attackedEffectCoroutine);
        }
        StartCoroutine(CoShowAttackedEffect());
    }
    IEnumerator CoShowAttackedEffect()
    {
        _spriteRenderer_Inside.color = new Color(1, 1, 1, 0.2f);
        _spriteRenderer_Outside.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.15f);
        _spriteRenderer_Inside.color = new Color(1, 1, 1, 1);
        _spriteRenderer_Outside.color = new Color(1, 1, 1, 1);
    }
}
