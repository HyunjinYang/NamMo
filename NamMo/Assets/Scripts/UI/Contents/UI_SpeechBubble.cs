using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpeechBubble : UI_Base
{
    Dictionary<int, Conversation> speechBubbleDict;
    enum Texts
    {
        Text_Speech
    }
    public override void Init()
    {
        if (_init) return;
        _init = true;
        Bind<TextMeshProUGUI>(typeof(Texts));
        speechBubbleDict = Managers.Data.TutorialSpeechBubbleDict;
    }
    public void SetPosAndText(Vector3 pos, int textNum)
    {
        gameObject.transform.position = pos;

        int languageId = 0;
        Conversation conversation = null;
        speechBubbleDict.TryGetValue(textNum, out conversation);
        Get<TextMeshProUGUI>((int)Texts.Text_Speech).text = conversation.scripts[languageId];
    }
}
