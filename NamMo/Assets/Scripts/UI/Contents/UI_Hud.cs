using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    [Header("Wave Gage Sprite")]
    [SerializeField] private List<Sprite> _waveGageSprites = new List<Sprite>();
    private List<UI_Heart> _hearts = new List<UI_Heart>();
    enum Texts
    {
    }
    enum Images
    {
        Image_WaveGage,

        Image_DashCooltime,
        Image_WaveCooltime
    }
    enum GameObjects
    {
        Hp
    }
    public override void Init()
    {
        if (_init) return;
        _init = true;

        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

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
            //Get<TextMeshProUGUI>((int)Texts.Text_RemainWaveCnt).text = (wave as GA_WaveDetect).RemainUseCnt.ToString();
        }
    }
    private void CreateHeart()
    {
        UI_Heart heart = Managers.UI.MakeSubItem<UI_Heart>(Get<GameObject>((int)GameObjects.Hp).transform);
        heart.Init();
        _hearts.Add(heart);
    }
    private void RefreshHp(float hp, float maxHp)
    {
        if(_hearts.Count < maxHp)
        {
            int makeCnt = (int)maxHp - _hearts.Count;
            for(int i = 0; i < makeCnt; i++)
            {
                CreateHeart();
            }
        }
        else if(_hearts.Count > maxHp)
        {
            int removeCnt = _hearts.Count - (int)maxHp;
            for(int i = 0; i < removeCnt; i++)
            {
                Destroy(_hearts[_hearts.Count - 1]);
                _hearts.RemoveAt(_hearts.Count - 1);
            }
        }
        for(int i = 0; i < maxHp; i++)
        {
            float value = Mathf.Clamp(hp - i, 0, 1);
            _hearts[i].SetHeart(value);
        }
    }
    private void RefreshDashCoolTime(float coolTime)
    {
        Get<Image>((int)Images.Image_DashCooltime).fillAmount = 0;
        Get<Image>((int)Images.Image_DashCooltime).DOFillAmount(1, coolTime).SetEase(Ease.Linear);
    }
    private void RefreshWaveRemainCnt(int remainUseCnt)
    {
        Get<Image>((int)Images.Image_WaveGage).sprite = _waveGageSprites[remainUseCnt];
    }
    private void RefreshWaveCoolTime(float coolTime)
    {
        Get<Image>((int)Images.Image_WaveCooltime).fillAmount = 0;
        Get<Image>((int)Images.Image_WaveCooltime).DOFillAmount(1, coolTime).SetEase(Ease.Linear);
    }
}
