using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Jump : GameAbility
{
    protected override void Init()
    {
        _asc.gameObject.GetComponent<PlayerMovement>().OnLandGround += EndAbility;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Attack);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Parrying);
        _asc.GetComponent<PlayerMovement>().StartJump();
    }
    public override void CancelAbility()
    {
        base.CancelAbility();
        _asc.GetComponent<PlayerMovement>().EndJump();
    }
}
