using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CutScene : UI_Base
{
    enum Images
    {
        Image_Bg,
        Image_CutScene
    }
    public Action OnFadeOutBgEnd;
    public Action OnShowCutSceneImageEnd;
    public Action OnHideCutSceneImageEnd;
    public Action OnFadeInBgEnd;
    public override void Init()
    {
        if (_init) return;
        _init = true;
        Bind<Image>(typeof(Images));
    }
    public void FadeOutBg(float duration = 1)
    {
        Get<Image>((int)Images.Image_Bg).DOFade(1, duration).OnComplete(() =>
        {
            if (OnFadeOutBgEnd != null) OnFadeOutBgEnd.Invoke();
        });
    }
    public void ShowCutSceneImage(float duration = 1)
    {
        Get<Image>((int)Images.Image_CutScene).DOFade(1, duration).OnComplete(() =>
        {
            if (OnShowCutSceneImageEnd != null) OnShowCutSceneImageEnd.Invoke();
        });
    }
    public void HideCutSceneImage(float duration = 1)
    {
        Get<Image>((int)Images.Image_CutScene).DOFade(0, duration).OnComplete(() =>
        {
            if (OnHideCutSceneImageEnd != null) OnHideCutSceneImageEnd.Invoke();
        });
    }
    public void FadeInBg(float duration = 1)
    {
        Get<Image>((int)Images.Image_Bg).DOFade(0, duration).OnComplete(() =>
        {
            if (OnFadeInBgEnd != null) OnFadeInBgEnd.Invoke();
        });
    }
}
