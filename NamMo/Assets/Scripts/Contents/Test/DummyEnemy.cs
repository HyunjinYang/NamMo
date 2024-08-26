using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    private Coroutine _attackedEffectCoroutine = null;
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
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
