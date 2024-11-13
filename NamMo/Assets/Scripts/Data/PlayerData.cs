using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : GameData
{
    public static bool Respawn = false;

    public bool isNewData = true;
    public float Hp = 10;
    public float MaxHp = 10;
    public Vector3 Position = Vector3.zero;
    public Define.Scene LocateScene = Define.Scene.Unknown;
    public List<Define.GameplayAbility> Abilities = new List<Define.GameplayAbility>()
    { 
        Define.GameplayAbility.GA_Jump,
        Define.GameplayAbility.GA_Dash,
        Define.GameplayAbility.GA_Attack,
        Define.GameplayAbility.GA_WaveDetect,
        Define.GameplayAbility.GA_AirAttack,
        Define.GameplayAbility.GA_DownJump,
        Define.GameplayAbility.GA_Block,
        Define.GameplayAbility.GA_Parrying,
        Define.GameplayAbility.GA_Hurt,
        Define.GameplayAbility.GA_Invincible,
        Define.GameplayAbility.GA_Charge,
        Define.GameplayAbility.GA_StrongAttack,
        Define.GameplayAbility.GA_Attack2,
        Define.GameplayAbility.GA_ParryingAttack,
    };
    public int WaveDetectCnt = 4;
    public override void Save()
    {
        RefreshData();
        base.Save();
    }
    public override void RefreshData()
    {
        PlayerController player = Managers.Scene.CurrentScene.Player;

        isNewData = false;
        if (player)
        {
            Hp = player.GetPlayerStat().Hp;
            MaxHp = player.GetPlayerStat().MaxHp;
            Position = player.transform.position;
            Abilities = player.GetASC().GetOwnedAbilities();

            GameAbility ability = player.GetASC().GetAbility(Define.GameplayAbility.GA_WaveDetect);
            if (ability)
            {
                GA_WaveDetect waveDetectAbility = ability as GA_WaveDetect;
                if (waveDetectAbility)
                {
                    WaveDetectCnt = waveDetectAbility.RemainUseCnt;
                }
            }
        }

        LocateScene = Managers.Scene.CurrentScene.SceneType;
    }
    public override void Clear()
    {
        PlayerData data = new PlayerData();
        data.Init(typeof(PlayerData).Name);
        Managers.Data.PlayerData = data;
    }
}
