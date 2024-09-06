using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : GameData
{
    public float Hp = 10;
    public float MaxHp = 10;
    public Vector3 Position;
    public Define.Scene LocateScene = Define.Scene.CaveScene;
    public List<Define.GameplayAbility> Abilities = new List<Define.GameplayAbility>();
    public int WaveDetectCnt = 4;
    public override void Save()
    {
        PlayerController player = Managers.Scene.CurrentScene.Player;
        GameAbility ability = player.GetASC().GetAbility(Define.GameplayAbility.GA_WaveDetect);
        if (ability)
        {
            GA_WaveDetect waveDetectAbility = ability as GA_WaveDetect;
            if (waveDetectAbility)
            {
                WaveDetectCnt = waveDetectAbility.RemainUseCnt;
            }
        }
       
        Hp = player.GetPlayerStat().Hp;
        MaxHp = player.GetPlayerStat().MaxHp;
        Position = player.transform.position;
        LocateScene = Managers.Scene.CurrentScene.SceneType;
        Abilities = player.GetASC().GetOwnedAbilities();
        base.Save();
    }
}
