using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class GA_Block : GameAbility
{
    [SerializeField] private float _perfectParryingTime;
    public bool IsPerfectParryingTiming { get; private set; } = false;
    public Action OnBlockStart;
    public Action OnBlockEnd;

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
        IsPerfectParryingTiming = false;

        if (OnBlockEnd != null) OnBlockEnd.Invoke();
    }
    private void HandleTriggeredObject(GameObject go)
    {
        if (IsPerfectParryingTiming)
        {
            if (_asc.IsExsistTag(Define.GameplayTag.Player_State_Hurt) == false)
            {
                // TODO 패링 기획에 따라 변경
                Debug.Log("Parrying");
                CancelAbility();
                Destroy(go);
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Parrying);
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
        IsPerfectParryingTiming = true;
        yield return new WaitForSeconds(_perfectParryingTime);
        IsPerfectParryingTiming = false;
        _parryingCoroutine = null;
    }
}
