using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct AttackInfo
{
    public float attackTime;
    public float attackMoment;
    public float attackRate;
    public float dashForce;
    public float dashTime;
}
public class GA_Attack : GameAbility
{
    [SerializeField] private List<AttackInfo> _comboAttackInfos = new List<AttackInfo>();
    private bool _reserveNextAttack = false;

    public Action<int> OnComboChanged;
    protected override void ActivateAbility()
    {
        if(_overlapCnt == 0 || _reserveNextAttack)
        {
            base.ActivateAbility();
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Dash);
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_DownJump);
            StartCoroutine(CoAttack());
        }
        else
        {
            _reserveNextAttack = true;
        }
    }
    protected override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_overlapCnt >= _comboAttackInfos.Count) return false;
        if (_reserveNextAttack) return false;
        return true;
    }
    public override void CancelAbility()
    {
        if (_reserveNextAttack)
        {
            _overlapCnt++;
            _reserveNextAttack = false;
        }
        _asc.gameObject.GetComponent<PlayerMovement>().CancelReserveDash(Define.DashType.AttackDash);
        EndAbility();
    }
    protected override void EndAbility()
    {
        if (_reserveNextAttack)
        {
            ActivateAbility();
        }
        else
        {
            base.EndAbility();
            if (OnComboChanged != null) OnComboChanged.Invoke(-1);
        }
    }
    IEnumerator CoAttack()
    {
        _reserveNextAttack = false;
        
        int currCombo = _overlapCnt - 1;
        if (OnComboChanged != null) OnComboChanged.Invoke(currCombo);

        AttackInfo currComboAttackInfo = _comboAttackInfos[currCombo];
        float additionalGravityTime = currComboAttackInfo.attackTime - currComboAttackInfo.attackMoment - currComboAttackInfo.dashTime;
        _asc.gameObject.GetComponent<PlayerMovement>().ReserveDash(currComboAttackInfo.attackMoment, currComboAttackInfo.dashForce, currComboAttackInfo.dashTime, Define.DashType.AttackDash, additionalGravityTime);
        yield return new WaitForSeconds(currComboAttackInfo.attackMoment);

        Debug.Log($"Attack : {currCombo}");

        yield return new WaitForSeconds(currComboAttackInfo.attackTime - currComboAttackInfo.attackMoment);
        EndAbility();
    }
}
