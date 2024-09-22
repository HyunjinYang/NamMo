using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_DownJump : GameAbility
{
    private GameObject _floor = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _floor.GetComponent<DownJumpFloor>().ChangeColliderMaskShortTime();
        _asc.gameObject.GetComponent<PlayerMovement>().OnLandGround += EndAbility;
        Managers.Sound.Play("Jump");
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        _floor = _asc.gameObject.GetComponent<PlayerMovement>().GetGroundFloor();
        if (_floor == null) return false;
        if (_floor.GetComponent<DownJumpFloor>() == null) return false;
        return true;
    }
    public override void CancelAbility()
    {
        base.CancelAbility();
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().OnLandGround -= EndAbility;
    }
}
