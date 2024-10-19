using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Charge : GameAbility
{
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_asc.gameObject.GetComponent<PlayerMovement>().IsGround == false) return false;
        return true;
    }
    protected override void CancelAbility()
    {
        base.CancelAbility();
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
}
