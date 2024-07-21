using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public Action<float> OnMoveInputChanged;
    public Action OnJumpInputPerformed;
    public Action OnJumpInputCanceled;
    public Action OnAttackInputPerformed;
    // SendMessage
    private void OnMove(InputValue value)
    {
        float moveVal = value.Get<float>();
        OnMoveInputChanged.Invoke(moveVal);
    }
    // Invoke
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
    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
            OnJumpInputPerformed.Invoke();
        }
        else if (context.canceled)
        {
            OnJumpInputCanceled.Invoke();
        }
    }
    public void HandleAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttackInputPerformed.Invoke();
        }
    }
}
