using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainScene : UI_Base
{
    private Action[] _actions = new Action[5];
    enum Images
    {
        NewGame,
        Continue,
        Remember,
        Setting,
        Exit
    }
    
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        
        Get<Image>((int)Images.NewGame).gameObject.BindEvent(NewGame,Define.UIEvent.Click);
        Get<Image>((int)Images.Continue).gameObject.BindEvent(Continue, Define.UIEvent.Click);
        Get<Image>((int)Images.Remember).gameObject.BindEvent(Remember, Define.UIEvent.Click);
        Get<Image>((int)Images.Setting).gameObject.BindEvent(Setting, Define.UIEvent.Click);
        Get<Image>((int)Images.Exit).gameObject.BindEvent(Exit, Define.UIEvent.Click);

        Get<Image>((int)Images.NewGame).gameObject.BindEvent((() => EnterCursor((int)Images.NewGame)), Define.UIEvent.PointerEnter);
        Get<Image>((int)Images.Continue).gameObject.BindEvent(()=>EnterCursor((int)Images.Continue), Define.UIEvent.PointerEnter);
        Get<Image>((int)Images.Remember).gameObject.BindEvent(()=>EnterCursor((int)Images.Remember), Define.UIEvent.PointerEnter);
        Get<Image>((int)Images.Setting).gameObject.BindEvent(()=>EnterCursor((int)Images.Setting), Define.UIEvent.PointerEnter);
        Get<Image>((int)Images.Exit).gameObject.BindEvent(()=>EnterCursor((int)Images.Exit), Define.UIEvent.PointerEnter);
    }

    private void InputKeyEvent()
    {
        
    }
    
    private void NewGame()
    {
        
    }

    protected override void Input()
    {
        base.Input();
        
    }

    private void Continue()
    {
        
    }

    private void Remember()
    {
        
    }
    private void Setting()
    {
        
    }

    private void Exit()
    {
        
    }

    private void EnterCursor(int idx)
    {
        
    }
    
}
