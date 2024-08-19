using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    enum Texts
    {
        Text_Hp,
        Text_RemainWaveCnt
    }
    enum Images
    {
        Image_DashCooltime,
        Image_WaveCooltime
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        PlayerStat ps = Managers.Scene.CurrentScene.Player.GetPlayerStat();
        RefreshHp(ps.Hp, ps.MaxHp);
        ps.OnHpChanged += RefreshHp;

        GameAbility dash = Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Dash);
        if (dash)
        {
            dash.OnCooltimeStart += RefreshDashCoolTime;
        }

        GameAbility wave = Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_WaveDetect);
        if (wave)
        {
            wave.OnCooltimeStart += RefreshWaveCoolTime;

            (wave as GA_WaveDetect).OnRemainUseCntChanged += RefreshWaveRemainCnt;
            Get<TextMeshProUGUI>((int)Texts.Text_RemainWaveCnt).text = (wave as GA_WaveDetect).RemainUseCnt.ToString();
        }
    }
    private void RefreshHp(float hp, float maxHp)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Hp).text = $"Hp : {hp} / {maxHp}";
    }
    private void RefreshDashCoolTime(float coolTime)
    {
        Get<Image>((int)Images.Image_DashCooltime).fillAmount = 0;
        Get<Image>((int)Images.Image_DashCooltime).DOFillAmount(1, coolTime).SetEase(Ease.Linear);
    }
    private void RefreshWaveRemainCnt(int remainUseCnt)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_RemainWaveCnt).text = remainUseCnt.ToString();
    }
    private void RefreshWaveCoolTime(float coolTime)
    {
        Get<Image>((int)Images.Image_WaveCooltime).fillAmount = 0;
        Get<Image>((int)Images.Image_WaveCooltime).DOFillAmount(1, coolTime).SetEase(Ease.Linear);
    }
}
