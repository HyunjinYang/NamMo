using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class GA_Block : GameAbility
{
    [SerializeField] private float _perfectParryingTime;
    public Action OnBlockStart;
    public Action OnBlockEnd;

    private bool _isPerfectParryingTiming = false;
    private Coroutine _parryingCoroutine = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        _asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
        _asc.GetPlayerController().GetBlockArea().ActiveBlockArea();
        _parryingCoroutine = StartCoroutine(CoChangeParryingTypeByTimeFlow());

        if (OnBlockStart != null) OnBlockStart.Invoke();
    }
    public override void CancelAbility()
    {
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();

        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
        _asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered -= HandleTriggeredObject;
        _asc.GetPlayerController().GetBlockArea().DeActiveBlockArea();

        if (_parryingCoroutine != null)
        {
            StopCoroutine(_parryingCoroutine);
            _parryingCoroutine = null;
        }
        _isPerfectParryingTiming = false;

        if (OnBlockEnd != null) OnBlockEnd.Invoke();
    }
    private void HandleTriggeredObject(GameObject go)
    {
        if (_isPerfectParryingTiming)
        {
            if (_asc.IsExsistTag(Define.GameplayTag.Player_State_Hurt) == false)
            {
                // TODO 패링 기획에 따라 변경
                CancelAbility();
                Destroy(go);
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Parrying);
            }
            else
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Hurt);
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Invincible);
            }
        }
        else
        {
            // TODO : 데미지 절반만 적용
            Debug.Log("타이밍 미스");
        }
    }
    IEnumerator CoChangeParryingTypeByTimeFlow()
    {
        _isPerfectParryingTiming = true;
        yield return new WaitForSeconds(_perfectParryingTime);
        _isPerfectParryingTiming = false;
        _parryingCoroutine = null;
    }
}
