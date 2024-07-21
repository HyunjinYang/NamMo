using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    PlayerController _pc;
    Rigidbody2D _rb;

    private float _horizontalMoveValue;
    private bool _isFacingRight = true;
    // TODO : ASC
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool _isJumping = false;

    private Action _reservedInputAction = null;

    public Action<bool> OnFlip;
    public Action OnLandGround;
    // -------------------- Unity Method --------------------
    #region Unity Method
    private void Awake()
    {
        if (_pc == null) _pc = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();

        _pc.OnMoveInputChanged += RefreshHorizontalMoveValue;
        _pc.OnJumpInputPerformed += StartJump;
        _pc.OnJumpInputCanceled += EndJump;

        // tmp
        _pc.OnAttackInputPerformed += (() => Dash(10f, 0.1f));
    }
    private void FixedUpdate()
    {
        if (_isDashing == false)
        {
            _rb.velocity = new Vector2(_horizontalMoveValue * _speed, _rb.velocity.y);
        }
        if (_isJumping && _rb.velocity.y <= 0)
        {
            if(IsGround())
            {
                _isJumping = false;
                if (OnLandGround != null) OnLandGround.Invoke();
            }
        }
    }
    #endregion
    // -------------------- Public Method --------------------
    #region Public Method
    public void StartJump()
    {
        _isJumping = true;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
    public void EndJump()
    {
        if (!_isJumping || _rb.velocity.y <= 0 || IsGround()) return;
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
    }
    public void Dash(float dashForce, float dashTime)
    {
        if (_isDashing) return;
        StartCoroutine(CoDash(dashForce, dashTime));
    }
    private bool IsGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
    #endregion
    // -------------------- Private Method --------------------
    #region Private Method
    private void RefreshHorizontalMoveValue(float value)
    {
        if (_isDashing)
        {
            _reservedInputAction = () =>
            {
                _horizontalMoveValue = value;
                CheckFlip();
            };
            return;
        }
        _horizontalMoveValue = value;
        CheckFlip();
    }
    private void CheckFlip()
    {
        if (!_isFacingRight && _horizontalMoveValue > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && _horizontalMoveValue < 0f)
        {
            Flip();
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        OnFlip.Invoke(_isFacingRight);
    }
    #endregion
    // -------------------- Coroutine --------------------
    IEnumerator CoDash(float dashForce, float dashTime)
    {
        _isDashing = true;
        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        _rb.gravityScale = originalGravity;
        _isDashing = false;
        if (_reservedInputAction != null)
        {
            _reservedInputAction.Invoke();
            _reservedInputAction = null;
        }
    }
}
