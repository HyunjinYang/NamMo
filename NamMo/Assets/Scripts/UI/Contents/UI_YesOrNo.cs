using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_YesOrNo : UI_Base
{
    enum Texts
    {
        Text_Request
    }
    enum Buttons
    {
        Button_Yes,
        Button_No
    }
    private int _cursor = 0;
    private Dictionary<Buttons, Action> _buttonActions = new Dictionary<Buttons, Action>();
    public override void Init()
    {
        if (_init) return;
        _init = true;

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        HighlightCurrentCursorButton();
    }
    protected override void Input(Define.KeyInput key)
    {
        if (key == Define.KeyInput.A)
        {
            _cursor = (_cursor - 1 + 2) % 2;
            HighlightCurrentCursorButton();
        }
        else if (key == Define.KeyInput.D)
        {
            _cursor = (_cursor + 1) % 2;
            HighlightCurrentCursorButton();
        }
        else if (key == Define.KeyInput.Enter)
        {
            Action action;
            if(_buttonActions.TryGetValue((Buttons)_cursor, out action))
            {
                action.Invoke();
            }
            else
            {
                Debug.Log("No Assigned Action");
            }
        }
    }
    public void SetRequestText(string text)
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Request).text = text;
    }
    public void AssignYesButtonEvent(Action action)
    {
        _buttonActions.Add(Buttons.Button_Yes, action);
    }
    public void AssignNoButtonEvent(Action action)
    {
        _buttonActions.Add(Buttons.Button_No, action);
    }
    private void HighlightCurrentCursorButton()
    {
        Get<Button>(_cursor).gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        Get<Button>((_cursor + 1) % 2).gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
}
