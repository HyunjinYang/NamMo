using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Tutorial : UI_Base
{
    Dictionary<Define.TutorialType, TutorialInfo> dict;
    enum Texts
    {
        Text_Tutorial,
    }
    public override void Init()
    {
        if (_init) return;
        _init = true;

        Bind<TextMeshProUGUI>(typeof(Texts));
        dict = Managers.Data.TutorialInfoDict;
    }
    public void SetTutorialText(Define.TutorialType tutorialType)
    {
        TutorialInfo tutorialInfo = dict[tutorialType];
        Get<TextMeshProUGUI>((int)Texts.Text_Tutorial).text = tutorialInfo.tutorialText[0];
    }
}
