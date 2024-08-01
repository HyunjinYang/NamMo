using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Hurt : GameAbility
{
    private float _dir = 0;
    public Action OnHurtStart;
    public Action OnHurtEnd;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Dash);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Attack);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_AirAttack);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_DownJump);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Block);

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
        yield return new WaitForSeconds(0.5f);
        EndAbility();
    }
}
