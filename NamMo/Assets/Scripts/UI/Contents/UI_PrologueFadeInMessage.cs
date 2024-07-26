using Data;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PrologueFadeInMessage : UI_Base
{
    enum Texts
    {
        Text_Script
    }
    enum Images
    {
        Image_Background
    }
    public Action OnPrologueFadeInEnd;
    public Action<int> OnFadeInScriptComplete;
    public Action<int> OnFadeOutScriptComplete;
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        ShowPrologueMessages();
        OnFadeInScriptComplete += FadeInScriptComplete;
        OnFadeOutScriptComplete += FadeOutScriptComplete;
    }
    private void ShowPrologueMessages()
    {
        DelayAction(3.0f, () => FadeInScript(0));
    }
    private void FadeInScript(int scriptNum)
    {
        PrologueFadeInScript scripts = null;
        int languageCode = (int)Define.Languages.Kor;
        if(Managers.Data.PrologueFadeInScriptDict.TryGetValue(scriptNum, out scripts))
        {
            Get<TextMeshProUGUI>((int)Texts.Text_Script).text = scripts.scripts[languageCode];
            Get<TextMeshProUGUI>((int)Texts.Text_Script).DOFade(1, 3).SetEase(Ease.InCubic).OnComplete(() => OnFadeInScriptComplete.Invoke(scriptNum));
        }
    }
    private void FadeOutScript(int scriptNum)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Script).DOFade(0, 1).OnComplete(() => OnFadeOutScriptComplete.Invoke(scriptNum));
    }
    private void FadeOutScreen()
    {
        Get<Image>((int)Images.Image_Background).DOFade(0, 1).OnComplete(
            () =>
            {
                if (OnPrologueFadeInEnd != null) 
                {
                    OnPrologueFadeInEnd.Invoke();
                    OnPrologueFadeInEnd = null;
                }
                Destroy(gameObject);
            });
    }
    private void FadeInScriptComplete(int scriptNum)
    {
        DelayAction(2f, () => FadeOutScript(scriptNum));
    }
    private void FadeOutScriptComplete(int scriptNum)
    {
        PrologueFadeInScript scripts = null;
        if (Managers.Data.PrologueFadeInScriptDict.TryGetValue(scriptNum + 1, out scripts))
        {
            DelayAction(2f, () => FadeInScript(scriptNum + 1));
        }
        else
        {
            DelayAction(2f, () => FadeOutScreen());
        }
    }
    private Coroutine DelayAction(float time, Action action)
    {
        return StartCoroutine(Managers.CoDelayAction(time, action));
    }
}
