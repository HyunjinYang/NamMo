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
