using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilitySystemComponent _asc;
    [SerializeField] private BlockArea _blockArea;
    [SerializeField] private List<Define.GameplayAbility> _abilities;
    public Action<float> OnMoveInputChanged;
    public Action OnAttackInputPerformed;
    private PlayerMovement _pm;
    private bool _pushDown = false;
    private void Awake()
    {
        _pm = gameObject.GetComponent<PlayerMovement>();
        if (_asc == null) _asc = GetComponent<AbilitySystemComponent>();
        foreach(var ability in _abilities)
        {
            _asc.GiveAbility(ability);
        }
    }
    public BlockArea GetBlockArea()
    {
        return _blockArea;
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
            if (_pushDown)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_DownJump);
            }
            else
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Jump);
            }
            
        }
        else if (context.canceled)
        {
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
        }
    }
    // 하단
    public void HandleDownInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pushDown = true;
        }
        else if (context.canceled)
        {
            _pushDown = false;
        }
    }
    // 공격
    public void HandleAttackInput(InputAction.CallbackContext context)
    {
        if(_pm.IsJumping || _pm.IsFalling)
        {
            if (context.performed)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_AirAttack);
            }
        }
        else
        {
            if (context.performed)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Attack);
            }
        }
    }
    // 파동탐지
    public void HandleWaveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_WaveDetect);
        }
    }
    // 패링
    public void HandleParryingInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Block);
        }
        else if (context.canceled)
        {
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Block);
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
