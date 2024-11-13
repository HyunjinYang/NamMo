using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Parrying : GameAbility
{
    [SerializeField] private float _parryingTime;

    private Coroutine _parryingCoroutine;
    private float _dir;
    private int _parriedAttackStrength;

    public List<BaseAttack> ParriedAttacks = new List<BaseAttack>();
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        _parryingCoroutine = StartCoroutine(CoParrying());
        ApplyBlockCancelAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        _asc.GetPlayerController().GetPlayerSound().PlayParryingSound();

        Managers.Scene.CurrentScene.ApplyTimeSlow(0.5f, 0.5f);
        //Camera.main.GetComponent<CameraController>().ShakeCamera(1.5f);
        Camera.main.GetComponent<CameraController>().ZoomCamera();

        float knockbackPower = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.GA_Parrying].reactValues[_parriedAttackStrength].knockbackPower;
        Managers.Scene.CurrentScene.Player.GetPlayerMovement().AddForce(new Vector2(_dir, 0), knockbackPower, 0.2f);

    }
    protected override void CancelAbility()
    {
        base.CancelAbility();
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();

        if (_parryingCoroutine != null)
        {
            StopCoroutine(_parryingCoroutine);
            _parryingCoroutine = null;
        }
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;

        ParriedAttacks.Clear();
    }
    public void SetParriedAttackInfo(float dir, int attackStrength)
    {
        _dir = dir;
        _parriedAttackStrength = attackStrength;
        float blockCancelTime = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.GA_Parrying].reactValues[_parriedAttackStrength].bindTime;
        BlockCancelTime = blockCancelTime;
    }
    IEnumerator CoParrying()
    {
        yield return new WaitForSecondsRealtime(_parryingTime);
        EndAbility();
    }
}
