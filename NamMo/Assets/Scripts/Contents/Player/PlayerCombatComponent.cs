using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamagedType
{
    Default,
    Block,
    Noknockback
}
public class PlayerCombatComponent : MonoBehaviour
{
    private PlayerController _pc;
    public void SetPlayerController(PlayerController pc)
    {
        _pc = pc;
    }
    public bool GetDamaged(/*TODO*/float damage, Vector3 attackPos, int attackStrength = 1)
    {
        if (_pc.GetASC().IsExsistTag(Define.GameplayTag.Player_State_Invincible))
        {
            // 무적상태일때 공격이 들어왔을 경우
            return false;
        }

        DamagedType damagedType = DamagedType.Default;

        if (_pc.GetASC().IsExsistTag(Define.GameplayTag.Player_Action_Block))
        {
            GA_Block blockAbility = _pc.GetASC().GetAbility(Define.GameplayAbility.GA_Block) as GA_Block;
            if (blockAbility.ReserveParrying)
            {
                return false;
            }
            // 패링 타이밍이 맞지 않았다면 데미지 절반 적용
            damage /= 2;
            StartCoroutine(CoHurtShortTime());
            damagedType = DamagedType.Block;
        }
        else
        {
            // 넉백, 피격ability
            if (_pc.GetASC().IsExsistTag(Define.GameplayTag.Player_Action_Wave) == false &&
                _pc.GetASC().IsExsistTag(Define.GameplayTag.Player_Action_StrongAttack) == false)
            {
                
                //(_pc.GetASC().GetAbility(Define.GameplayAbility.GA_Hurt) as GA_Hurt).SetKnockBackForce(force);
                _pc.GetASC().TryActivateAbilityByTag(Define.GameplayAbility.GA_Hurt);
            }
            else
            {
                //damagedType = DamagedType.Noknockback;
            }
        }
        _pc.GetASC().TryActivateAbilityByTag(Define.GameplayAbility.GA_Invincible);
        StartCoroutine(CoShowAttackedEffect());
        _pc.GetPlayerStat().ApplyDamage(damage);

        float dir = 1;
        if (transform.position.x < attackPos.x) dir = -1;
        float knockbackPower;
        if (damagedType == DamagedType.Default)
        {
            knockbackPower = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.None].reactValues[attackStrength].knockbackPower;
            //knockbackPower *= dir;
            _pc.GetPlayerMovement().AddForce(new Vector2(dir, 0), knockbackPower, 0.2f);
        }
        else if (damagedType == DamagedType.Block)
        {
            knockbackPower = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.GA_Block].reactValues[attackStrength].knockbackPower;
            //knockbackPower *= dir;
            _pc.GetPlayerMovement().AddForce(new Vector2(dir, 0), knockbackPower, 0.2f);
        }
        return true;
    }
    IEnumerator CoHurtShortTime()
    {
        _pc.GetASC().AddTag(Define.GameplayTag.Player_State_Hurt);
        yield return new WaitForSecondsRealtime(0.2f);
        _pc.GetASC().RemoveTag(Define.GameplayTag.Player_State_Hurt);
    }
    IEnumerator CoShowAttackedEffect()
    {
        _pc.GetPlayerSprite().GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        _pc.GetPlayerSprite().GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
}
