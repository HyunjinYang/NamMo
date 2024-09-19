using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.PrologueFadeInScript> PrologueFadeInScriptDict { get; private set; } = new Dictionary<int, PrologueFadeInScript>();
    public Dictionary<int, Data.Conversation> ConversationDict { get; private set; } = new Dictionary<int, Conversation>();
    public Dictionary<int, Data.Conversation> TutorialSpeechBubbleDict { get; private set; } = new Dictionary<int, Conversation>();
    public Dictionary<int, Data.CharacterInfo> CharacterInfoDict { get; private set; } = new Dictionary<int, Data.CharacterInfo>();
    public Dictionary<Define.TutorialType, Data.TutorialInfo> TutorialInfoDict { get; private set; } = new Dictionary<Define.TutorialType, Data.TutorialInfo>();
    public PlayerData PlayerData;
    public void Init()
    {
        PrologueFadeInScriptDict = LoadJson<Data.PrologueFadeInScriptData, int, Data.PrologueFadeInScript>("PrologueFadeInScriptData").MakeDict();

        ConversationDict = LoadJson<Data.ConversationData, int, Data.Conversation>("ConversationData").MakeDict();
        TutorialSpeechBubbleDict = LoadJson<Data.ConversationData, int, Data.Conversation>("TutorialSpeechBubbleData").MakeDict();

        CharacterInfoDict = LoadJson<Data.CharacterInfoData, int, Data.CharacterInfo>("CharacterInfoData").MakeDict();
        TutorialInfoDict = LoadJson<Data.TutorialInfoData, Define.TutorialType, Data.TutorialInfo>("TutorialInfoData").MakeDict();

        PlayerData = GameData.Load<PlayerData>();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    public void SaveAllData()
    {
        PlayerData.Save();
    }
    public void ClearAllData()
    {
        PlayerData.Clear();
    }
}
