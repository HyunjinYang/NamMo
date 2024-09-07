using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.MainScene;

        // tmp
        //Gamepad pad = Gamepad.current;
        //if (pad != null)
        //{
        //    pad.SetMotorSpeeds(0f, 0f);
        //}

        Managers.Sound.Play("Title", Define.Sound.Bgm);
    }
    public override void Clear()
    {
    }
}
