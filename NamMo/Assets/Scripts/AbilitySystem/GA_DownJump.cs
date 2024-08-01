using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_DownJump : GameAbility
{
    private GameObject _floor = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Attack);
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Parrying);
        _floor.GetComponent<DownJumpFloor>().ChangeColliderMaskShortTime();
        _asc.gameObject.GetComponent<PlayerMovement>().OnLandGround += EndAbility;
    }
    protected override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        _floor = _asc.gameObject.GetComponent<PlayerMovement>().GetGroundFloor();
        if (_floor == null) return false;
        if (_floor.GetComponent<DownJumpFloor>() == null) return false;
        return true;
    }
    public override void CancelAbility()
    {
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().OnLandGround -= EndAbility;
    }
}
