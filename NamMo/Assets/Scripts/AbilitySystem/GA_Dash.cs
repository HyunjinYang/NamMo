using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Dash : GameAbility
{
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;

    protected override void Init()
    {
        _asc.gameObject.GetComponent<PlayerMovement>().OnDashEnd += EndAbility;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        Managers.Sound.Play("Dash");
        _asc.gameObject.GetComponent<PlayerMovement>().Dash(_dashForce, _dashTime, Define.DashType.DefaultDash);
    }
    public override void CancelAbility()
    {
        base.CancelAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CancelDash(Define.DashType.DefaultDash);
    }
}
