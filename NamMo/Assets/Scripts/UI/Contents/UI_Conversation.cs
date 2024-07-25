using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Base
{
    [SerializeField] private float _defaultTypingSpeed;
    [SerializeField] private float _fastTypingSpeed;
    enum Texts
    {
        Text_Conversation,
        Text_CharacterName
    }
    enum Images
    {
        Image_CharacterPortrait
    }
    enum GameObjects
    {
        Elements
    }
    enum TypingState
    {
        EndTyping,
        DefaultTyping,
        FastTyping,
        NoTyping
    }
    private Dictionary<int, Action> _flowActions = new Dictionary<int, Action>();
    private TypingState _currentTypingState = TypingState.EndTyping;
    private int _currentConversationNum = 0;
    private int _typingCursor = 0;
    private StringBuilder _currentConversation = new StringBuilder();
    private Coroutine _typingCoroutine = null;
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        ShowInfos();
    }
    protected override void Input(Define.KeyInput key)
    {
        PushButton(key);
    }
    private void ShowInfos()
    {
        Get<GameObject>((int)GameObjects.Elements).gameObject.SetActive(true);
        _typingCursor = 0;
        _currentConversation.Clear();
        int characterInfoId = Managers.Data.ConversationDict[_currentConversationNum].characterId;
        SetCharacterPortrait(characterInfoId);
        SetCharacterName(characterInfoId);
        ShowConversationText();
    }
    private void SetCharacterPortrait(int characterInfoId)
    {
        string path = Managers.Data.CharacterInfoDict[characterInfoId].iconPath;
        // TODO
    }
    private void SetCharacterName(int characterInfoId)
    {
        int languageCode = (int)Define.Languages.Kor;
        string name = Managers.Data.CharacterInfoDict[characterInfoId].names[languageCode];
        Get<TextMeshProUGUI>((int)Texts.Text_CharacterName).text = name;
    }
    private void ShowConversationText()
    {
        _currentTypingState = TypingState.DefaultTyping;
        int languageCode = (int)Define.Languages.Kor;
        string message = Managers.Data.ConversationDict[_currentConversationNum].scripts[languageCode];
        _typingCoroutine = StartCoroutine(CoTypingConversationText(message, _defaultTypingSpeed));
    }
    private void PushButton(Define.KeyInput key)
    {
        if (key == Define.KeyInput.Enter)
        {
            if (_currentTypingState == TypingState.EndTyping)
            {
                HandleFlowAction();
            }
            else if (_currentTypingState == TypingState.DefaultTyping)
            {
                StopCoroutine(_typingCoroutine);
                int languageCode = (int)Define.Languages.Kor;
                string message = Managers.Data.ConversationDict[_currentConversationNum].scripts[languageCode];
                _typingCoroutine = StartCoroutine(CoTypingConversationText(message, _fastTypingSpeed));
                _currentTypingState = TypingState.FastTyping;
            }
        }
    }
    private void HandleFlowAction()
    {
        Action flowAction = null;
        if (_flowActions.TryGetValue(_currentConversationNum, out flowAction))
        {
            _currentTypingState = TypingState.NoTyping;
            flowAction.Invoke();
        }
        else
        {
            Conversation conversation = null;
            if (Managers.Data.ConversationDict.TryGetValue(_currentConversationNum + 1, out conversation) == false)
            {
                Debug.Log("End Conversation");
                return;
            }
            ShowNextInfos();
        }
    }
    public void RegisterFlowAction(int num, Action action)
    {
        Action flowAction = null;
        if(_flowActions.TryGetValue(num, out flowAction))
        {
            Debug.LogError("Already Exsist Action");
        }
        else
        {
            _flowActions.Add(num, action);
        }
    }
    public void ShowNextInfos()
    {
        _currentConversationNum++;
        ShowInfos();
    }
    public void HideElements()
    {
        Get<GameObject>((int)GameObjects.Elements).gameObject.SetActive(false);
    }
    IEnumerator CoTypingConversationText(string message, float speed)
    {
        while(_typingCursor < message.Length)
        {
            _currentConversation.Append(message[_typingCursor++]);
            Get<TextMeshProUGUI>((int)Texts.Text_Conversation).text = _currentConversation.ToString();
            yield return new WaitForSeconds(1 / (speed * 10));
        }
        _typingCoroutine = null;
        _currentTypingState = TypingState.EndTyping;
    }
}
