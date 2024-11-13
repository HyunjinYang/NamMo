using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Attack2 : GameAbility
{
    [SerializeField] private float _attackForce;
    private Vector2 _attackDirection;
    private bool _canAirDash = true;
    protected override void Init()
    {
        _asc.GetPlayerController().GetPlayerMovement().OnLandGround += RefreshCanAirDash;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        StartCoroutine(CoAttack());
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        return base.CanActivateAbility();
    }
    protected override void CancelAbility()
    {
        base.CancelAbility();
        //_asc.GetPlayerController().GetPlayerMovement().CancelAddForceBlockMove();
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
    }
    public void SetAttackDirection(Vector2 dir)
    {
        _attackDirection = dir;
        if(dir.magnitude < 0.5f)
        {
            if (_asc.GetPlayerController().GetPlayerMovement().IsFacingRight) _attackDirection = Vector2.right;
            else _attackDirection = Vector2.left;
        }
        _attackDirection = _attackDirection.normalized;
    }
    private void RefreshCanAirDash()
    {
        _canAirDash = true;
    }
    IEnumerator CoAttack()
    {
        _asc.GetPlayerController().GetPlayerSound().PlayAttackSound();
        Vector2 dir = _attackDirection;
        _asc.GetPlayerController().GetAttackArea().SetAttackInfo(_asc.GetPlayerController().gameObject, 1);
        _asc.GetPlayerController().GetAttackArea().SetDirection(dir);
        _asc.GetPlayerController().GetAttackArea().Attack();
        // 땅에 있을 때
        if (_asc.GetPlayerController().GetPlayerMovement().IsGround || _canAirDash)
        {
            _canAirDash = false;
            if (_asc.GetPlayerController().GetPlayerMovement().IsGround)
            {
                if (dir.y < 0) dir.y = 0;
            }
            if (dir.y > 0.7f)
            {
                _asc.GetPlayerController().GetPlayerMovement().AddForce(dir, _attackForce * dir.magnitude, 0.1f + Mathf.Abs(dir.x / 14f));
            }
            else
            {
                _asc.GetPlayerController().GetPlayerMovement().AddForce(dir, _attackForce * dir.magnitude, 0.2f);
            }
            
        }
        // 공중에 있을 때
        else
        {
            if (dir.y > 0)
            {
                if(dir.x < 0.7f && dir.x > -0.7f)
                {
                    dir.y = dir.y * 0.25f;
                    _asc.GetPlayerController().GetPlayerMovement().AddForce(dir, _attackForce * dir.magnitude, 0.15f);
                }
                else
                {
                    dir.y = 0;
                    dir = dir.normalized;
                    _asc.GetPlayerController().GetPlayerMovement().AddForce(dir, _attackForce * dir.magnitude, 0.2f);
                }
            }
            else
            {
                _asc.GetPlayerController().GetPlayerMovement().AddForce(dir, _attackForce * dir.magnitude, 0.2f);
            }
        }
        
        yield return new WaitForSeconds(0.2f);
        EndAbility();
    }
}
