using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_Kjh : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UI_Hud>();
    }
    public override void Clear()
    {
    }
}