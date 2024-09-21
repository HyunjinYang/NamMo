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
    public Action OnFadeOutScreenEnd;
    public Action OnFadeInScreenEnd;
    public Action<int> OnFadeInScriptComplete;
    public Action<int> OnFadeOutScriptComplete;
    private bool _detectInput = false;
    private int _scriptNum = 0;
    public override void Init()
    {
        if(_init) return;
        _init = true;
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
    }
    protected override void Input(Define.KeyInput key)
    {
        if (key == Define.KeyInput.Enter && _detectInput)
        {
            _detectInput = false;
            FadeOutScript(_scriptNum);
        }
    }
    public void ShowPrologueMessages()
    {
        Get<Image>((int)Images.Image_Background).DOFade(1, 0);
        OnFadeInScriptComplete += FadeInScriptComplete;
        OnFadeOutScriptComplete += FadeOutScriptComplete;
        DelayAction(2.0f, () => FadeInScript(0));
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
    public void FadeInScreen(float duration = 1)
    {
        Get<Image>((int)Images.Image_Background).DOFade(0, duration).OnComplete(
            () =>
            {
                if (OnFadeInScreenEnd != null)
                {
                    OnFadeInScreenEnd.Invoke();
                    OnFadeInScreenEnd = null;
                }
                Destroy(gameObject);
            });
    }
    public void FadeOutScreen(float duration = 1)
    {
        Get<Image>((int)Images.Image_Background).DOFade(1, duration).OnComplete(
            () =>
            {
                if (OnFadeOutScreenEnd != null)
                {
                    OnFadeOutScreenEnd.Invoke();
                    OnFadeOutScreenEnd = null;
                }
            });
    }
    private void FadeInScriptComplete(int scriptNum)
    {
        _detectInput = true;
        _scriptNum = scriptNum;
        //DelayAction(2f, () => FadeOutScript(scriptNum));
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
            DelayAction(2f, () => FadeInScreen());
        }
    }
    private Coroutine DelayAction(float time, Action action)
    {
        return StartCoroutine(Managers.CoDelayAction(time, action));
    }
}
