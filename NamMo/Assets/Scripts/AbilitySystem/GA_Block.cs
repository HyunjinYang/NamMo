using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Block : GameAbility
{
    [SerializeField] private float _perfectParryingTime;
    [SerializeField] private float _blockTime;
    private bool _isPerfectParryingTiming = false;
    private bool _reserveNextCombo = false;

    public Action<int> OnBlockComboChanged;

    private Coroutine _cacluateParryingTimingCoroutine = null;
    private Coroutine _blockCoroutine = null;

    private bool _reserveParrying = false;
    public bool ReserveParrying { get { return _reserveParrying; } }
    protected override void Init()
    {
        base.Init();
        _asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
    }
    protected override void ActivateAbility()
    {
        if (_overlapCnt == 0 || _reserveNextCombo)
        {
            base.ActivateAbility();
            _cacluateParryingTimingCoroutine = StartCoroutine(CoChangeParryingTypeByTimeFlow());
            _blockCoroutine = StartCoroutine(CoBlock());
        }
        else
        {
            _reserveNextCombo = true;
        }
    }
    protected override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_reserveNextCombo) return false;
        return true;
    }
    public override void CancelAbility()
    {
        if (_reserveNextCombo)
        {
            _overlapCnt++;
            _reserveNextCombo = false;
        }
        EndAbility();
    }
    protected override void EndAbility()
    {
        if (_cacluateParryingTimingCoroutine != null)
        {
            StopCoroutine(_cacluateParryingTimingCoroutine);
            _cacluateParryingTimingCoroutine = null;
        }
        if (_blockCoroutine != null)
        {
            StopCoroutine(_blockCoroutine);
            _blockCoroutine = null;
        }
        _isPerfectParryingTiming = false;

        if (_reserveNextCombo)
        {
            ActivateAbility();
        }
        else
        {
            base.EndAbility();
            _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
            //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered -= HandleTriggeredObject;
            _asc.GetPlayerController().GetBlockArea().DeActiveBlockArea();

            if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke(_overlapCnt);
        }
    }
    private bool canceled = false;
    private void HandleTriggeredObject(GameObject go)
    {
        if (_isPerfectParryingTiming)
        {
            if (_asc.IsExsistTag(Define.GameplayTag.Player_State_Hurt) == false)
            {
                // TODO 패링 기획에 따라 변경
                if (go.GetComponent<BaseProjectile>())
                {
                    BaseProjectile projectile = go.GetComponent<BaseProjectile>();
                    projectile.SetProjectileInfo(null, projectile.Speed, projectile.Damage, _asc.GetPlayerController().gameObject);
                    projectile.Parried();
                }
                _reserveParrying = true;
                if (canceled == false)
                {
                    StartCoroutine(CoCancelAbility());
                }
            }
        }
    }
    IEnumerator CoBlock()
    {
        _reserveNextCombo = false;
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
        _asc.GetPlayerController().GetBlockArea().ActiveBlockArea();
        Debug.Log($"Block Combo : {(_overlapCnt - 1) % 3 + 1}");
        if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke((_overlapCnt - 1) % 3 + 1);

        yield return new WaitForSeconds(_blockTime);

        _blockCoroutine = null;
        EndAbility();
    }
    IEnumerator CoChangeParryingTypeByTimeFlow()
    {
        _isPerfectParryingTiming = true;
        yield return new WaitForSeconds(_perfectParryingTime);
        _isPerfectParryingTiming = false;
        _cacluateParryingTimingCoroutine = null;
    }
    IEnumerator CoCancelAbility()
    {
        canceled = true;
        yield return new WaitForEndOfFrame();
        CancelAbility();
        RefreshCoolTime();
        _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Parrying);
        canceled = false;
        _reserveParrying = false;
    }
}
