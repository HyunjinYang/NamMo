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
        _asc.gameObject.GetComponent<PlayerMovement>().OnDashCanceled += CancelAbility;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().Dash(_dashForce, _dashTime);
    }
    public override void CancelAbility()
    {
        Debug.Log("Cancel Dash");
    }
}
