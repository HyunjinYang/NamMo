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
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        ShowPrologueMessages();
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
            Get<TextMeshProUGUI>((int)Texts.Text_Script).DOFade(1, 5).OnComplete(
                () =>
                {
                    DelayAction(0f, ()=>FadeOutScript(scriptNum));
                });
        }
    }
    private void FadeOutScript(int scriptNum)
    {
        PrologueFadeInScript scripts = null;
        if (Managers.Data.PrologueFadeInScriptDict.TryGetValue(scriptNum + 1, out scripts))
        {
            Get<TextMeshProUGUI>((int)Texts.Text_Script).DOFade(0, 1).OnComplete(
                () =>
                {
                    DelayAction(2f, () => FadeInScript(scriptNum + 1));
                });
        }
        else
        {
            Get<TextMeshProUGUI>((int)Texts.Text_Script).DOFade(0, 1).OnComplete(
                () =>
                {
                    DelayAction(2f, () => FadeOutScreen());
                });
        }
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
    private void DelayAction(float time, Action action)
    {
        StartCoroutine(CoDelayAction(time, action));
    }
    IEnumerator CoDelayAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
