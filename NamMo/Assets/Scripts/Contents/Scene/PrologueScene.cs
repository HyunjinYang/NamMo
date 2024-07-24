using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.PrologueScene;

        UI_PrologueFadeInMessage prologueFadeIn = Managers.UI.ShowUI<UI_PrologueFadeInMessage>();
        prologueFadeIn.OnPrologueFadeInEnd += StartPrologue;
    }
    private void StartPrologue()
    {
        Debug.Log("StartPrologue");
    }
    public override void Clear()
    {
    }
}
