using Enemy.Boss.MiniBoss;
using Enemy.MelEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_ParryingAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attackMoment;
    private Enemy.Enemy _targetEnemy = null;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        StartCoroutine(CoAttack());
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
    public bool CanParryingAttack(BaseAttack parriedAttack)
    {
        if (CanActivateAbility() == false) return false;

        if (parriedAttack == null) return false;
        if (parriedAttack as CloseAttack == null) return false;
        if (parriedAttack.Attacker == null) return false;

        GameObject attacker = parriedAttack.Attacker;
        // tmp : 일단 일반근접만, TODO
        if (attacker.GetComponent<MelEnemy>() == null) return false;
        if (attacker.GetComponent<MelEnemy>().stateMachine._CurrentState as GroggyState == null) return false;

        _targetEnemy = attacker.GetComponent<Enemy.Enemy>();
        return true;
    }
    IEnumerator CoAttack()
    {
        yield return new WaitForSecondsRealtime(_attackMoment);
        if (_targetEnemy)
        {
            if(_targetEnemy as MiniBossEnemy)
            {
                _targetEnemy.Hit(1);
            }
            else
            {
                _targetEnemy.Hit(1000);
            }
            _targetEnemy = null;
        }
        _asc.GetPlayerController().GetPlayerSound().PlayAttackSound();
        Camera.main.GetComponent<CameraController>().ShakeCamera(1.5f);
        yield return new WaitForSecondsRealtime(_attackTime - _attackMoment);
        EndAbility();
    }
}
