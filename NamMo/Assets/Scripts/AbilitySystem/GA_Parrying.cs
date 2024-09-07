using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Parrying : GameAbility
{
    [SerializeField] private float _parryingTime;

    private Coroutine _parryingCoroutine;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _parryingCoroutine = StartCoroutine(CoParrying());
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        Managers.Sound.Play("Parrying1");
        // TODO : 패링 
        Camera.main.transform.DOShakePosition(0.15f, new Vector3(0.25f, 0.25f, 0), 50);
        Camera.main.DOOrthoSize(10, 0.5f);
        Time.timeScale = 0.5f;
    }
    public override void CancelAbility()
    {
        base.CancelAbility();
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
        Camera.main.DOOrthoSize(12, 0.5f);
        Time.timeScale = 1.0f;
    }
    IEnumerator CoParrying()
    {
        yield return new WaitForSeconds(_parryingTime);
        EndAbility();
    }
}
