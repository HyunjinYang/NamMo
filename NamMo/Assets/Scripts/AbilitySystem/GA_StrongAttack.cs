using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_StrongAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    private Coroutine _strongAttackCoroutine = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        _strongAttackCoroutine = StartCoroutine(CoStrongAttack());
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_asc.gameObject.GetComponent<PlayerMovement>().IsGround == false) return false;
        return true;
    }
    public override void CancelAbility()
    {
        base.CancelAbility();
        if (_strongAttackCoroutine != null) StopCoroutine(_strongAttackCoroutine);
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
    IEnumerator CoStrongAttack()
    {
        yield return new WaitForSeconds(_attackTime);
        _strongAttackCoroutine = null;
        EndAbility();
    }
}
