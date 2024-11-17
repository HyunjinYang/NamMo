using Enemy.Boss.MiniBoss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_ParryingAttack : GameAbility
{
    [SerializeField] private float _attackTime;
    [SerializeField] private float _attackMoment;
    public Enemy.Enemy TargetEnemy = null;
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
    IEnumerator CoAttack()
    {
        yield return new WaitForSecondsRealtime(_attackMoment);
        if (TargetEnemy)
        {
            if(TargetEnemy as MiniBossEnemy)
            {
                TargetEnemy.Hit(1);
            }
            else
            {
                TargetEnemy.Hit(1000);
            }
            TargetEnemy = null;
        }
        _asc.GetPlayerController().GetPlayerSound().PlayAttackSound();
        Camera.main.GetComponent<CameraController>().ShakeCamera(1.5f);
        yield return new WaitForSecondsRealtime(_attackTime - _attackMoment);
        EndAbility();
    }
}
