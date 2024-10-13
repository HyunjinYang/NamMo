using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.UI;

public class UI_MainScene : UI_Base
{
    private Action[] _actions = new Action[5];
    private int cursor = 0;
    private const int BUTTON_COUNT = 5;
    private bool isSaveDataCheck;
    private bool langageTest;
    enum Images
    {
        NewGame,
        Continue,
        Remember,
        Setting,
        Exit
    }

    enum Texts
    {
        NewGame_text,
        Continue_text,
        Remember_text,
        Setting_text,
        Exit_text
    }
    
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        
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

        _actions[0] = NewGame;
        _actions[1] = Continue;
        _actions[2] = Remember;
        _actions[3] = Setting;
        _actions[4] = Exit;
        
        cursor = 0;
        SetColor(Get<TextMeshProUGUI>(cursor).gameObject, "#FF0000", false);
    }

    

    protected override void Input(Define.KeyInput key)
    {
        if (key == Define.KeyInput.D)
        {
            Debug.Log("오른쪽");
        }
        else if (key == Define.KeyInput.A)
        {
            Debug.Log("왼쪽");
        }
        else if (key == Define.KeyInput.S)
        {
            EnterCursor((cursor + 1) % BUTTON_COUNT);
        }
        else if (key == Define.KeyInput.W)
        {
            EnterCursor((cursor - 1 + BUTTON_COUNT) % BUTTON_COUNT);
        }
        else
        {
            _actions[cursor].Invoke();
        }
    }
    private void NewGame()
    { 
        Debug.Log("NewGame");
        if (Managers.Data.PlayerData.isNewData)
        {
            // TODO : 처음 씬으로 이동
            Debug.Log("New Game 1");
            Managers.Scene.LoadScene(Define.Scene.TestStartScene);
        }
        else
        {
            // TODO : 게임 데이터 날리고 처음 씬으로 이동
            Debug.Log("New Game 2");
            Managers.Data.ClearAllData();
            Managers.Scene.LoadScene(Define.Scene.TestStartScene);
        }
    }

    private void Continue()
    {
        Debug.Log("Continue");
        if (Managers.Data.PlayerData.isNewData)
        {
            Debug.Log("Continue Game 1");
            Debug.Log("저장된 데이터가 없습니다.");
        }
        else
        {
            Debug.Log("Continue Game 2");
            Managers.Scene.LoadScene(Managers.Data.PlayerData.LocateScene);
        }
    }

    private void Remember()
    {
        Debug.Log("Remember");
    }
    private void Setting()
    {
        if (!langageTest)
        {
            LoadLocale("en");
            langageTest = true;
        }
        else
        {
            LoadLocale("ko");
            langageTest = false;
        }

        Debug.Log("Setting");
    }

    private void Exit()
    {
        Debug.Log("Exit");
    }
    private void SetColor(GameObject gameObject, string color, bool isImageCheck)
    {
        Color newcolor;
        if (ColorUtility.TryParseHtmlString(color, out newcolor))
        {
            if (!isImageCheck)
                gameObject.GetComponent<TextMeshProUGUI>().color = newcolor;
            else
                Util.FindChild<Image>(gameObject).color = newcolor;
        }
    }
    
    public void LoadLocale(string languageIdentifier) {
        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);
        for(int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++) {
            Locale aLocale = LocalizationSettings.AvailableLocales.Locales[i];
            LocaleIdentifier anIdentifier = aLocale.Identifier;
            if(anIdentifier == localeCode) {
                LocalizationSettings.SelectedLocale = aLocale;
            }
        }
    }
    private void EnterCursor(int nextidx)
    {
        SetColor(Get<TextMeshProUGUI>(cursor).gameObject, "#000000", false);

        cursor = nextidx;
        
        SetColor(Get<TextMeshProUGUI>(cursor).gameObject, "#FF0000", false);
    }
    
}
