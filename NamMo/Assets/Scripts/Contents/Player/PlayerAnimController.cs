using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] PlayerMovement _pm;
    [SerializeField] Animator _animator;

    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isDasing = false;
    private bool _isWaveDetecting = false;
    private bool _isAirAttacking = false;
    private bool _isBlocking = false;
    private bool _isParrying = false;
    private bool _isHurting = false;

    private float _moveDir = 0;
    private int _attackCombo = -1;
    private void Start()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_pm)
        {
            _pm.OnStartJump += StartJump;
            _pm.OnStartFall += StartFall;
            _pm.OnLandGround += Land;
            _pm.OnWalk += Walk;
            _pm.OnDashStart += StartDash;
            _pm.OnDashEnd += EndDash;
            AbilitySystemComponent asc = _pm.gameObject.GetComponent<AbilitySystemComponent>();
            GA_Attack attackAbility = asc.GetAbility(Define.GameplayAbility.GA_Attack) as GA_Attack;
            if (attackAbility)
            {
                attackAbility.OnAttackComboChanged += ComboAttack;
            }
            GA_WaveDetect waveDetectAbility = asc.GetAbility(Define.GameplayAbility.GA_WaveDetect) as GA_WaveDetect;
            if (waveDetectAbility)
            {
                waveDetectAbility.OnWaveStart += WaveDetectStart;
                waveDetectAbility.OnWaveEnd += WaveDetectEnd;
            }
            GA_AirAttack airAttackAbility = asc.GetAbility(Define.GameplayAbility.GA_AirAttack) as GA_AirAttack;
            if (airAttackAbility)
            {
                airAttackAbility.OnAirAttackStart += AirAttackStart;
                airAttackAbility.OnAirAttackEnd += AirAttackEnd;
            }
            GA_Block blockAbility = asc.GetAbility(Define.GameplayAbility.GA_Block) as GA_Block;
            if (blockAbility)
            {
                blockAbility.OnBlockStart += BlockStart;
                blockAbility.OnBlockEnd += BlockEnd;
            }
            GA_Parrying parryingAbility = asc.GetAbility(Define.GameplayAbility.GA_Parrying) as GA_Parrying;
            if (parryingAbility)
            {
                parryingAbility.OnParryingStart += ParryingStart;
                parryingAbility.OnParryingEnd += ParryingEnd;
            }
            GA_Hurt hurtAbility = asc.GetAbility(Define.GameplayAbility.GA_Hurt) as GA_Hurt;
            if (hurtAbility)
            {
                hurtAbility.OnHurtStart += HurtingStart;
                hurtAbility.OnHurtEnd += HurtingEnd;
            }
        }
    }
    private void StartJump()
    {
        if (_moveDir != 0)
        {
            _moveDir = 0;
            _animator.SetBool("IsWalking", false);
        }
        _isJumping = true;
        _animator.SetBool("IsJumping", _isJumping);
    }
    private void StartFall()
    {
        if (_moveDir != 0)
        {
            _moveDir = 0;
            _animator.SetBool("IsWalking", false);
        }
        _isFalling = true;
        _animator.SetBool("IsFalling", _isFalling);
    }
    private void Land()
    {
        _isJumping = false;
        _isFalling = false;
        _animator.SetBool("IsJumping", _isJumping);
        _animator.SetBool("IsFalling", _isFalling);
        _animator.SetBool("IsLanding", true);
        StartCoroutine(CoDelayAction(
            () =>
            {
                _animator.SetBool("IsLanding", false);
            }));
    }
    private void Walk(float dir)
    {
        if (_moveDir != dir)
        {
            _moveDir = dir;
            if (_moveDir == 0)
            {
                _animator.SetBool("IsWalking", false);
            }
            else
            {
                _animator.SetBool("IsWalking", true);
            }
        }
    }
    private void StartDash()
    {
        _isDasing = true;
        _animator.SetBool("IsDashing", _isDasing);
    }
    private void EndDash()
    {
        _isDasing = false;
        _animator.SetBool("IsDashing", _isDasing);
    }
    private void ComboAttack(int combo)
    {
        _attackCombo = combo;
        _animator.SetInteger("AttackCombo", _attackCombo);
    }
    private void WaveDetectStart()
    {
        _isWaveDetecting = true;
        _animator.SetBool("IsWaveDetecting", _isWaveDetecting);
    }
    private void WaveDetectEnd()
    {
        _isWaveDetecting = false;
        _animator.SetBool("IsWaveDetecting", _isWaveDetecting);
    }
    private void AirAttackStart()
    {
        _isAirAttacking = true;
        _animator.SetBool("IsAirAttacking", _isAirAttacking);
    }
    private void AirAttackEnd()
    {
        _isAirAttacking = false;
        _animator.SetBool("IsAirAttacking", _isAirAttacking);
    }
    private void BlockStart()
    {
        _isBlocking = true;
        _animator.SetBool("IsBlocking", _isBlocking);
    }
    private void BlockEnd()
    {
        _isBlocking = false;
        _animator.SetBool("IsBlocking", _isBlocking);
    }
    private void ParryingStart()
    {
        _isParrying = true;
        _animator.SetBool("IsParrying", _isParrying);
    }
    private void ParryingEnd()
    {
        _isParrying = false;
        _animator.SetBool("IsParrying", _isParrying);
    }
    private void HurtingStart()
    {
        _isHurting = true;
        _animator.SetBool("IsHurting", _isHurting);
    }
    private void HurtingEnd()
    {
        _isHurting = false;
        _animator.SetBool("IsHurting", _isHurting);
    }
    IEnumerator CoDelayAction(Action action)
    {
        yield return null;
        action.Invoke();
    }
}
