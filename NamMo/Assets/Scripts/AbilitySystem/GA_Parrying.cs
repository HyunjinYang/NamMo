using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Parrying : GameAbility
{
    [SerializeField] private float _parryingTime;
    public Action OnParryingStart;
    public Action OnParryingEnd;

    private Coroutine _parryingCoroutine;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _parryingCoroutine = StartCoroutine(CoParrying());
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        // TODO : ÆÐ¸µ 
        Camera.main.transform.DOShakePosition(0.15f, new Vector3(0.25f, 0.25f, 0), 50);
        Camera.main.DOOrthoSize(9, 0.5f);
        Time.timeScale = 0.5f;
        if (OnParryingStart != null) OnParryingStart.Invoke();
    }
    public override void CancelAbility()
    {
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();

        if (_parryingCoroutine != null)
        {
            StopCoroutine(_parryingCoroutine);
            _parryingCoroutine = null;
        }
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
        Camera.main.DOOrthoSize(10, 0.5f);
        Time.timeScale = 1.0f;
        if (OnParryingEnd != null) OnParryingEnd.Invoke();
    }
    IEnumerator CoParrying()
    {
        yield return new WaitForSeconds(_parryingTime);
        EndAbility();
    }
}
