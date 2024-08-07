using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Hurt : GameAbility
{
    [SerializeField] private float _hurtTime;
    private float _dir = 0;
    public Action OnHurtStart;
    public Action OnHurtEnd;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _asc.GetPlayerController().GetPlayerMovement().CanMove = false;
        _asc.GetPlayerController().GetPlayerMovement().KnockBack(_dir);
        StartCoroutine(CoHurt());

        if (OnHurtStart != null) OnHurtStart.Invoke();
    }
    protected override void EndAbility()
    {
        base.EndAbility();

        _asc.GetPlayerController().GetPlayerMovement().CanMove = true;

        if (OnHurtEnd != null) OnHurtEnd.Invoke();
    }
    public void SetKnockBackDirection(float dir)
    {
        _dir = dir;
    }
    IEnumerator CoHurt()
    {
        yield return new WaitForSeconds(_hurtTime);
        EndAbility();
    }
}
