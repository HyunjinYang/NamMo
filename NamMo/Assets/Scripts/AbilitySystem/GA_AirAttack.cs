using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_AirAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _attack1Moment;
    [SerializeField] private float _attack2Moment;
    public Action OnAirAttackStart;
    public Action OnAirAttackEnd;
    private Coroutine _airAttackCoroutine = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        if (OnAirAttackStart != null) OnAirAttackStart.Invoke();
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Dash);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Parrying);
        _airAttackCoroutine = StartCoroutine(CoAirAttack());
    }
    public override void CancelAbility()
    {
        if (_airAttackCoroutine != null)
        {
            StopCoroutine(_airAttackCoroutine);
            EndAbility();
        }
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _airAttackCoroutine = null;
        if (OnAirAttackEnd != null) OnAirAttackEnd.Invoke();
    }
    IEnumerator CoAirAttack()
    {
        yield return new WaitForSeconds(_attack1Moment);
        Debug.Log("AirAttack1");
        yield return new WaitForSeconds(_attack2Moment - _attack1Moment);
        Debug.Log("AirAttack2");
        yield return new WaitForSeconds(_attackTime - _attack2Moment);
        EndAbility();
    }
}
