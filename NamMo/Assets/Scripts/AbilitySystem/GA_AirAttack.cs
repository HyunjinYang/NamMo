using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_AirAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attack1Rate;
    [SerializeField] private float _attack2Rate;
    [SerializeField] private float _attack1Moment;
    [SerializeField] private float _attack2Moment;
    [SerializeField] private Vector2 _attackRange1;
    [SerializeField] private Vector2 _attackOffset1;
    [SerializeField] private Vector2 _attackRange2;
    [SerializeField] private Vector2 _attackOffset2;
    private Coroutine _airAttackCoroutine = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _airAttackCoroutine = StartCoroutine(CoAirAttack());
        _asc.GetPlayerController().GetAttackArea().OnHitted += HandleTriggeredObject;

    }
    public override void CancelAbility()
    {
        base.CancelAbility();
        if (_airAttackCoroutine != null)
        {
            StopCoroutine(_airAttackCoroutine);
            EndAbility();
        }
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.GetPlayerController().GetAttackArea().OnHitted -= HandleTriggeredObject;
        _airAttackCoroutine = null;
    }
    private void HandleTriggeredObject(GameObject go)
    {
        Debug.Log("Attack Hit");
    }
    IEnumerator CoAirAttack()
    {
        yield return new WaitForSeconds(_attack1Moment);

        _asc.GetPlayerController().GetAttackArea().SetAttackInfo(_asc.GetPlayerController().gameObject, _attack1Rate);
        _asc.GetPlayerController().GetAttackArea().SetAttackRange(_attackRange1, _attackOffset1);
        _asc.GetPlayerController().GetAttackArea().ActiveAttackArea();

        yield return new WaitForFixedUpdate();

        _asc.GetPlayerController().GetAttackArea().DeActiveAttackArea();

        yield return new WaitForSeconds(_attack2Moment - _attack1Moment);

        _asc.GetPlayerController().GetAttackArea().SetAttackInfo(_asc.GetPlayerController().gameObject, _attack2Rate);
        _asc.GetPlayerController().GetAttackArea().SetAttackRange(_attackRange2, _attackOffset2);
        _asc.GetPlayerController().GetAttackArea().ActiveAttackArea();

        yield return new WaitForFixedUpdate();

        _asc.GetPlayerController().GetAttackArea().DeActiveAttackArea();

        yield return new WaitForSeconds(_attackTime - _attack2Moment);
        _asc.FlushReservedAbility();
        _airAttackCoroutine = null;
        EndAbility();
    }
}
