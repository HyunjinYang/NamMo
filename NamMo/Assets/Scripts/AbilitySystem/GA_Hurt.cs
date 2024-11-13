using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Hurt : GameAbility
{
    [SerializeField] private float _hurtTime;
    private float _knockBackforce = 0;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _asc.GetPlayerController().GetPlayerMovement().CanMove = false;
        //_asc.GetPlayerController().GetPlayerMovement().KnockBack(_knockBackforce);
        //Managers.Sound.Play("Attacked1");
        _asc.GetPlayerController().GetPlayerSound().PlayHittedSound();
        StartCoroutine(CoHurt());
    }
    protected override void EndAbility()
    {
        base.EndAbility();

        _asc.GetPlayerController().GetPlayerMovement().CanMove = true;
    }
    public void SetKnockBackForce(float force)
    {
        _knockBackforce = force;
    }
    IEnumerator CoHurt()
    {
        yield return new WaitForSecondsRealtime(_hurtTime);
        EndAbility();
    }
}
