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
    [SerializeField] private Vector2 _attackRange1;
    [SerializeField] private Vector2 _attackOffset1;
    [SerializeField] private Vector2 _attackRange2;
    [SerializeField] private Vector2 _attackOffset2;
    public Action OnAirAttackStart;
    public Action OnAirAttackEnd;
    private Coroutine _airAttackCoroutine = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        if (OnAirAttackStart != null) OnAirAttackStart.Invoke();
        _airAttackCoroutine = StartCoroutine(CoAirAttack());
        _asc.GetPlayerController().GetAttackArea().OnAttackAreaTriggerEntered += HandleTriggeredObject;

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
        _asc.GetPlayerController().GetAttackArea().OnAttackAreaTriggerEntered -= HandleTriggeredObject;
        _airAttackCoroutine = null;
        if (OnAirAttackEnd != null) OnAirAttackEnd.Invoke();
    }
    private void HandleTriggeredObject(GameObject go)
    {
        Debug.Log("Attack Hit");
        go.GetComponent<DummyEnemy>().Damaged();
    }
    IEnumerator CoAirAttack()
    {
        yield return new WaitForSeconds(_attack1Moment);

        _asc.GetPlayerController().GetAttackArea().SetAttackRange(_attackRange1, _attackOffset1);
        _asc.GetPlayerController().GetAttackArea().ActiveAttackArea();

        yield return new WaitForFixedUpdate();

        _asc.GetPlayerController().GetAttackArea().DeActiveAttackArea();

        yield return new WaitForSeconds(_attack2Moment - _attack1Moment);

        _asc.GetPlayerController().GetAttackArea().SetAttackRange(_attackRange2, _attackOffset2);
        _asc.GetPlayerController().GetAttackArea().ActiveAttackArea();

        yield return new WaitForFixedUpdate();

        _asc.GetPlayerController().GetAttackArea().DeActiveAttackArea();

        yield return new WaitForSeconds(_attackTime - _attack2Moment);
        _airAttackCoroutine = null;
        EndAbility();
    }
}
