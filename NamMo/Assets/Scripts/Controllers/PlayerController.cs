using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilitySystemComponent _asc;
    [SerializeField] private List<Define.GameplayAbility> _abilities;
    public Action<float> OnMoveInputChanged;
    public Action OnAttackInputPerformed;
    private void Awake()
    {
        if (_asc == null) _asc = GetComponent<AbilitySystemComponent>();
        foreach(var ability in _abilities)
        {
            _asc.GiveAbility(ability);
        }
    }
    // 이동
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        if (context.started)
        {
        }
        else if (context.performed)
        {
            OnMoveInputChanged.Invoke(value);
        }
        else if (context.canceled)
        {
            OnMoveInputChanged.Invoke(value);
        }
    }
    // 점프
    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Jump);
        }
        else if (context.canceled)
        {
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
        }
    }
    // 공격
    public void HandleAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Attack);
        }
    }
    // 파동탐지
    public void HandleWaveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleWaveInput");
        }
    }
    // 패링
    public void HandleParryingInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleParryingInput");
        }
    }
    // 대쉬
    public void HandleDashInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleDashInput");
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Dash);
        }
    }
    // 상호작용
    public void HandleInteractionInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleInteractionInput");
        }
    }
    // 아이템1
    public void HandleUseItem1Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleUseItem1Input");
        }
    }
    // 아이템2
    public void HandleUseItem2Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleUseItem2Input");
        }
    }
    
}
