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
    private Dictionary<int, Conversation> _conversationDict;
    private Dictionary<int, Action> _flowActions = new Dictionary<int, Action>();
    private TypingState _currentTypingState = TypingState.EndTyping;
    private int _currentConversationNum = 0;
    private int _typingCursor = 0;
    private StringBuilder _currentConversation = new StringBuilder();
    private Coroutine _typingCoroutine = null;
    public override void Init()
    {
        if (_init) return;
        _init = true;
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
        int characterInfoId = _conversationDict[_currentConversationNum].characterId;
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
        string message = _conversationDict[_currentConversationNum].scripts[languageCode];
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
                string message = _conversationDict[_currentConversationNum].scripts[languageCode];
                _currentTypingState = TypingState.FastTyping;
                _typingCoroutine = StartCoroutine(CoTypingConversationText(message, _fastTypingSpeed));
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
            if (_conversationDict.TryGetValue(_currentConversationNum + 1, out conversation) == false)
            {
                Debug.Log("End Conversation");
                return;
            }
            ShowNextInfos();
        }
    }
    public void SetConversationType(Define.ConversationType conversationType)
    {
        if (conversationType == Define.ConversationType.Prologe)
        {
            _conversationDict = Managers.Data.ConversationDict;
        }
        else if (conversationType == Define.ConversationType.SpawnMonsterTest)
        {
            _conversationDict = Managers.Data.NPCConversationDict_Test;
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
