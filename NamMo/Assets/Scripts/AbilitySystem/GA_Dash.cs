using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Dash : GameAbility
{
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _invincibleTime;

    private Coroutine _invincibleCoroutine = null;
    private int _originExcludeLayer;
    protected override void Init()
    {
        _asc.gameObject.GetComponent<PlayerMovement>().OnDashEnd += EndAbility;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        //Managers.Sound.Play("Dash");
        _asc.GetPlayerController().GetPlayerSound().PlayDashSound();
        _asc.gameObject.GetComponent<PlayerMovement>().Dash(_dashForce, _dashTime, Define.DashType.DefaultDash);

        _invincibleCoroutine = StartCoroutine(CoApplyInvincible());

        _originExcludeLayer = _asc.GetPlayerController().GetComponent<Collider2D>().excludeLayers;
        _asc.GetPlayerController().GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask("Enemy");
    }
    protected override void CancelAbility()
    {
        base.CancelAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CancelDash(Define.DashType.DefaultDash);
        //EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        if (_invincibleCoroutine != null)
        {
            StopCoroutine(_invincibleCoroutine);
            _asc.RemoveTag(Define.GameplayTag.Player_State_Invincible);
            _invincibleCoroutine = null;
        }
        _asc.GetPlayerController().GetComponent<Collider2D>().excludeLayers = _originExcludeLayer;
    }
    IEnumerator CoApplyInvincible()
    {
        _asc.AddTag(Define.GameplayTag.Player_State_Invincible);
        yield return new WaitForSeconds(_invincibleTime);
        _asc.RemoveTag(Define.GameplayTag.Player_State_Invincible);
        _invincibleCoroutine = null;
    }
}
