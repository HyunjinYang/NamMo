using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] protected GameObject _CharacterSprite;

    [SerializeField] protected float _speed;
    [SerializeField] private float _jumpForce;
    
    PlayerController _pc;
    protected Rigidbody2D _rb;

    private float _horizontalMoveValue;
    private float _originalGravity;
    private bool _isFacingRight = true;
    private bool _needCheckFrontGround = false;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isFalling = false;
    [SerializeField] private bool _isKnockBacking = false;
    private bool _canMove = true;

    private Action _reservedInputAction = null;
    private Coroutine _dashCoroutine = null;
    private Coroutine _reserveDashCoroutine = null;

    [SerializeField] private bool _reserveDash;
    
    public Action<bool> OnFlip;
    public Action OnLandGround;
    public Action OnDashStart;
    public Action OnDashEnd;
    public Action OnDashCanceled;
    public Action OnStartJump;
    public Action OnStartFall;
    public Action<float> OnWalk;
    public bool IsJumping {  get { return _isJumping; } }
    public bool IsDashing { get { return _isDashing; } }
    public bool IsFalling { get { return _isFalling; } }
    public bool CanMove
    {
        get
        {
            return _canMove;
        }
        set
        {
            _canMove = value;
            //_rb.velocity = new Vector2(0, _rb.velocity.y);
            if (_reservedInputAction != null && _reserveDash == false)
            {
                _reservedInputAction.Invoke();
                _reservedInputAction = null;
            }
        }
    }
    // -------------------- Unity Method --------------------
    #region Unity Method
    protected virtual void Awake()
    {
        if (_pc == null) _pc = GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();

        _pc.OnMoveInputChanged += RefreshHorizontalMoveValue;

        _originalGravity = 4f;
    }
    protected virtual void FixedUpdate()
    {
        if (_isDashing == false && _reserveDash == false)
        {
            {
                float horizontalValue = _horizontalMoveValue;
                if (_canMove == false) horizontalValue = 0f;
                if (_isJumping || _isFalling || _isKnockBacking)
                {
                    float x = Mathf.Lerp(_rb.velocity.x, horizontalValue * _speed, 0.08f);
                    _rb.velocity = new Vector2(x, _rb.velocity.y);
                }
                else
                {
                    _rb.velocity = new Vector2(horizontalValue * _speed, _rb.velocity.y);
                }
            }
            if(_isJumping == false && _isFalling == false)
            {
                if (OnWalk != null) OnWalk.Invoke(_horizontalMoveValue);
            }
        }
        if (_rb.velocity.y <= 0f && !_isFalling && !IsGround())
        {
            _isFalling = true;
            if (OnStartFall != null) OnStartFall.Invoke();
        }
        if ((_isFalling || _isJumping) && _rb.velocity.y == 0)
        {
            if (IsGround())
            {
                _isJumping = false;
                _isFalling = false;
                if (OnLandGround != null) OnLandGround.Invoke();
            }
        }
        if(_isDashing && _needCheckFrontGround)
        {
            float offset = 1f;
            if (_isFacingRight == false) offset *= -1;
            if (IsGround(offset) == false)
            {
                _rb.velocity = Vector2.zero;
                _needCheckFrontGround = false;
            }
        }
    }
    #endregion
    // -------------------- Private Method --------------------
    #region Private Method
    private void RefreshHorizontalMoveValue(float value)
    {
        if (_isDashing || _reserveDash || _canMove == false)
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
    protected virtual void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = _pc.GetPlayerSprite().transform.localScale;
        localScale.x *= -1f;
        _pc.GetPlayerSprite().transform.localScale = localScale;

        if (OnFlip != null) OnFlip.Invoke(_isFacingRight);
    }
    private void EndDash(Define.DashType dashType)
    {
        if(_reserveDash == false)
        {
            _rb.gravityScale = _originalGravity;
        }
        _isDashing = false;
        _needCheckFrontGround = false;
        if (_reservedInputAction != null && _reserveDash == false)
        {
            _reservedInputAction.Invoke();
            _reservedInputAction = null;
        }
        if (dashType == Define.DashType.DefaultDash)
        {
            OnDashEnd.Invoke();
        }
        _dashCoroutine = null;
    }
    #endregion
    // -------------------- Public Method --------------------
    #region Public Method
    public void StartJump()
    {
        _isJumping = true;
        float jumForce = _jumpForce;
        if (_isDashing)
        {
            _rb.gravityScale = _originalGravity;
        }
        _rb.velocity = new Vector2(_rb.velocity.x, jumForce);
        if (OnStartJump != null) OnStartJump.Invoke();
    }
    public void EndJump()
    {
        if (!_isJumping || _rb.velocity.y <= 0 || IsGround()) return;
        _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
    }
    public void Dash(float dashForce, float dashTime, Define.DashType dashType, float additionalNoGravityTime = 0)
    {
        if (_dashCoroutine != null) CancelDash(dashType);
        _dashCoroutine = StartCoroutine(CoDash(dashForce, dashTime, dashType,additionalNoGravityTime));
    }
    public void CancelReserveDash(Define.DashType dashType)
    {
        if (_reserveDashCoroutine != null)
        {
            _reserveDash = false;
            StopCoroutine(_reserveDashCoroutine);
            _reserveDashCoroutine = null;
            _rb.gravityScale = _originalGravity;
        }
        else
        {
            CancelDash(dashType);
        }
    }
    public void CancelDash(Define.DashType dashType)
    {
        if (_dashCoroutine == null) return;
        StopCoroutine(_dashCoroutine);
        _dashCoroutine = null;
        EndDash(dashType);
        if (dashType == DashType.DefaultDash)
        {
            if (OnDashCanceled != null) OnDashCanceled.Invoke();
        }
    }
    public void KnockBack(float force, float power = 5)
    {
        _rb.AddForce(new Vector2(force, 1f) * power, ForceMode2D.Impulse);
        StartCoroutine(CoKnockBack(0.5f));
    }
    public bool IsGround(float offset = 0)
    {
        return Physics2D.OverlapCircle(_groundCheck.position + new Vector3(offset, 0, 0), 0.2f, _groundLayer);
    }
    public GameObject GetGroundFloor()
    {
        Collider2D floor = Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
        if(floor == null) return null;
        return floor.gameObject;
    }
    public void ReserveDash(float delayTime, float dashForce, float dashTime, Define.DashType dashType, float additionalNoGravityTime = 0)
    {
        _reserveDash = true;
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0f;
        _reserveDashCoroutine = StartCoroutine(CoReserveDash(delayTime, dashForce, dashTime, dashType, additionalNoGravityTime));
    }
    #endregion
    // -------------------- Coroutine --------------------
    IEnumerator CoDash(float dashForce, float dashTime, Define.DashType dashType, float additionalNoGravityTime)
    {
        if (dashType == DashType.DefaultDash)
        {
            if (OnDashStart != null) OnDashStart.Invoke();
        }
        else
        {
            _needCheckFrontGround = true;
        }
        _isDashing = true;
        if (_reserveDash) _reserveDash = false;
        _rb.gravityScale = 0f;
        if (_isFacingRight)
        {
            _rb.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        }
        else
        {
            _rb.velocity = new Vector2(-transform.localScale.x * dashForce, 0f);
        }
        yield return new WaitForSeconds(dashTime);
        if(!(_isJumping || _isFalling))
        {
            _rb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(additionalNoGravityTime);
        EndDash(dashType);
    }
    IEnumerator CoReserveDash(float delayTime, float dashForce, float dashTime, Define.DashType dashType, float additionalNoGravityTime = 0)
    {
        yield return new WaitForSeconds(delayTime);
        _reserveDashCoroutine = null;
        Dash(dashForce, dashTime, dashType, additionalNoGravityTime);
    }
    IEnumerator CoKnockBack(float time)
    {
        _isKnockBacking = true;
        yield return new WaitForSeconds(time);
        _isKnockBacking = false;
    }
}
