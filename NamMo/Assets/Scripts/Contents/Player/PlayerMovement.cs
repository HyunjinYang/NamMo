using Enemy;
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
    [SerializeField] protected float _overlappedSpeed;
    [SerializeField] protected float _currentSpeed;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float _groundCheckRadius;

    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;
    [SerializeField] private bool isOnSlope;
    private bool canWalkOnSlope;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    [SerializeField] private bool _isGround;
    [SerializeField] private bool _isFrontGround;

    PlayerController _pc;
    protected Rigidbody2D _rb;

    private float _horizontalMoveValue;
    private float _originalGravity;
    private float _dashForce;
    private bool _isFacingRight = true;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isFalling = false;
    [SerializeField] private bool _isKnockBacking = false;
    private bool _canMove = true;

    private Action _reservedInputAction = null;
    private Coroutine _dashCoroutine = null;
    private Coroutine _reserveDashCoroutine = null;

    [SerializeField] private bool _reserveDash;
    private int _overlapEnemyCnt = 0;
    
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
    public bool IsGround { get { return _isGround; } }
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

        _currentSpeed = _speed;
        _originalGravity = 4f;

        colliderSize = GetComponent<CapsuleCollider2D>().size;
    }
    protected virtual void FixedUpdate()
    {
        _isGround = CheckGround();
        _isFrontGround = CheckGround(0.75f);
        SlopeCheck();
        CheckCurrentState();
        ApplyMove();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DummyEnemy>() == null 
            && collision.gameObject.GetComponent<Enemy.Enemy>() == null) return;
        _overlapEnemyCnt++;
        if (_overlapEnemyCnt > 0)
        {
            _currentSpeed = _overlappedSpeed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DummyEnemy>() == null
            && collision.gameObject.GetComponent<Enemy.Enemy>() == null) return;
        _overlapEnemyCnt--;
        if (_overlapEnemyCnt == 0)
        {
            _currentSpeed = _speed;
        }
    }
    #endregion
    // -------------------- Private Method --------------------
    #region Private Method
    private void RefreshHorizontalMoveValue(float value)
    {
        if (_dashCoroutine != null || _reserveDash || _canMove == false)
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
    private bool CheckGround(float offset = 0)
    {
        Vector3 offsetV;
        if (_isFacingRight)
        {
            offsetV = new Vector3(slopeNormalPerp.x, slopeNormalPerp.y, 0) * offset * -1;
        }
        else
        {
            offsetV = new Vector3(slopeNormalPerp.x, slopeNormalPerp.y, 0) * offset;
        }
        Collider2D col = Physics2D.OverlapCircle(_groundCheck.position + offsetV, _groundCheckRadius, _groundLayer);
        if (col == null)
        {
            return false;
        }
        if (col.gameObject.GetComponent<DownJumpFloor>() == null)
        {
            return true;
        }
        if (col.gameObject.GetComponent<DownJumpFloor>().IgnoreGroundCheck)
        {
            return false;
        }
        return true;
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
    private void CheckCurrentState()
    {
        // ³«ÇÏ
        if (_rb.velocity.y <= 0f && !_isFalling && !_isGround)
        {
            _isFalling = true;
            if (OnStartFall != null) OnStartFall.Invoke();
        }
        // ÂøÁö
        if ((_isFalling || _isJumping) && _rb.velocity.y <= 1f)
        {
            if (_isGround)
            {
                _isJumping = false;
                _isFalling = false;
                if (OnLandGround != null) OnLandGround.Invoke();
                Managers.Sound.Play("Land");
            }
        }

    }
    private void ApplyMove()
    {
        
        if (_dashCoroutine == null && _reserveDash == false)
        {
            {
                float horizontalValue = _horizontalMoveValue;
                if (_canMove == false) horizontalValue = 0f;
                if (_isJumping || _isFalling || _isKnockBacking)
                {
                    float x = Mathf.Lerp(_rb.velocity.x, horizontalValue * _currentSpeed, 0.08f);
                    _rb.velocity = new Vector2(x, _rb.velocity.y);
                }
                else
                {
                    if (isOnSlope && canWalkOnSlope)
                    {
                        _rb.velocity = new Vector2(_currentSpeed * slopeNormalPerp.x * -horizontalValue, _currentSpeed * slopeNormalPerp.y * -horizontalValue);
                    }
                    else
                    {
                        _rb.velocity = new Vector2(horizontalValue * _currentSpeed, _rb.velocity.y);
                    }
                    
                }
            }
            if (_isJumping == false && _isFalling == false)
            {
                if (OnWalk != null) OnWalk.Invoke(_horizontalMoveValue);
            }
        }

        if (_isDashing)
        {
            if (_isGround && _isFrontGround == false)
            {
                _rb.velocity = Vector2.zero;
            }
            else
            {
                if (_isFacingRight)
                {
                    _rb.velocity = new Vector2(slopeNormalPerp.x, slopeNormalPerp.y) * _dashForce * -1;
                }
                else
                {
                    _rb.velocity = new Vector2(slopeNormalPerp.x, slopeNormalPerp.y) * _dashForce;
                }
            }
        }
    }
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, colliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, _pc.GetPlayerSprite().transform.right, slopeCheckDistance, _groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -_pc.GetPlayerSprite().transform.right, slopeCheckDistance, _groundLayer);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, _groundLayer);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }
        else
        {
            slopeNormalPerp = Vector2.left;

        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && _horizontalMoveValue == 0.0f && _isDashing == false)
        {
            _rb.sharedMaterial = fullFriction;
        }
        else
        {
            _rb.sharedMaterial = noFriction;
        }
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
        //if (!_isJumping || _rb.velocity.y <= 0 || IsGround()) return;
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
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        _rb.AddForce(new Vector2(force, 1f) * power, ForceMode2D.Impulse);
        StartCoroutine(CoKnockBack(0.5f));
    }
    //public bool IsGround(float offset = 0)
    //{
    //    return Physics2D.OverlapCircle(_groundCheck.position + new Vector3(offset, 0, 0), _groundCheckRadius, _groundLayer);
    //}
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
        _dashForce = dashForce;
        if (dashType == DashType.DefaultDash)
        {
            if (OnDashStart != null) OnDashStart.Invoke();
        }
        _isDashing = true;
        if (_reserveDash) _reserveDash = false;
        _rb.gravityScale = 0f;
        yield return new WaitForSeconds(dashTime);
        _isDashing = false;
        if (!(_isJumping || _isFalling))
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        Vector3 offsetV;
        if (_isFacingRight)
        {
            offsetV = new Vector3(slopeNormalPerp.x, slopeNormalPerp.y, 0)  * -1;
        }
        else
        {
            offsetV = new Vector3(slopeNormalPerp.x, slopeNormalPerp.y, 0);
        }
        Gizmos.DrawWireSphere(_groundCheck.position + offsetV, _groundCheckRadius);
    }
}
