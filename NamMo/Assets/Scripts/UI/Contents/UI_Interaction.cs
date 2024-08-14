using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Interaction : UI_Base
{
    enum Texts
    {
        Text_Interaction
    }

    public override void Init()
    {
        if (_init) return;
        _init = true;
        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    public void SetInteractionText(Model_InteractionTextData model)
    {
        string text = model.InteractionText;
        Get<TextMeshProUGUI>((int)Texts.Text_Interaction).text = text;
    }
    public void Close()
    {
        if(gameObject) Destroy(gameObject);
    }
}
