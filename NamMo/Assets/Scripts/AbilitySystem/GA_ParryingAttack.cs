using Enemy.Boss.MiniBoss;
using Enemy.MelEnemy;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree_Enemy;
using UnityEngine;

public class GA_ParryingAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attackMoment;
    private TestEnemy _targetEnemy = null;
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
        // tmp : �ϴ� �Ϲݱ�����, TODO
        if (attacker.GetComponent<TestEnemy>() == null) return false;
        if (attacker.GetComponent<TestEnemy>()._EnemyState != Define.EnemyState.Groggy) return false;

        _targetEnemy = attacker.GetComponent<TestEnemy>();
        return true;
    }
    IEnumerator CoAttack()
    {
        yield return new WaitForSecondsRealtime(_attackMoment);
        if (_targetEnemy)
        {
            if(_targetEnemy as TestEnemy)
            {
                _targetEnemy.Hit(1000);
            }
            else
            {
                _targetEnemy.Hit(1);
            }
            _targetEnemy = null;
        }
        _asc.GetPlayerController().GetPlayerSound().PlayAttackSound();
        Camera.main.GetComponent<CameraController>().ShakeCamera(1.5f);
        yield return new WaitForSecondsRealtime(_attackTime - _attackMoment);
        EndAbility();
    }
}
