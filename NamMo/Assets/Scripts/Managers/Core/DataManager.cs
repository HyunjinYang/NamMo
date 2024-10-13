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
    public Dictionary<int, Data.Conversation> NPCConversationDict_Test { get; private set; } = new Dictionary<int, Conversation>();
    public Dictionary<int, Data.Conversation> TutorialSpeechBubbleDict { get; private set; } = new Dictionary<int, Conversation>();
    public Dictionary<int, Data.CharacterInfo> CharacterInfoDict { get; private set; } = new Dictionary<int, Data.CharacterInfo>();
    public Dictionary<Define.TutorialType, Data.TutorialInfo> TutorialInfoDict { get; private set; } = new Dictionary<Define.TutorialType, Data.TutorialInfo>();
    public Dictionary<int, Data.Enemy> EnemyDict { get; private set; } = new Dictionary<int, Data.Enemy>();
    public Dictionary<Define.Scene, Data.StageEnemy> StageEnemyDict { get; private set; } = new Dictionary<Define.Scene, Data.StageEnemy>();
    public PlayerData PlayerData;
    public EnemyData EnemyData;
    public void Init()
    {
        PrologueFadeInScriptDict = LoadJson<Data.PrologueFadeInScriptData, int, Data.PrologueFadeInScript>("PrologueFadeInScriptData").MakeDict();

        ConversationDict = LoadJson<Data.ConversationData, int, Data.Conversation>("ConversationData").MakeDict();
        NPCConversationDict_Test = LoadJson<Data.ConversationData, int, Data.Conversation>("NPCConversationData_Test").MakeDict();

        TutorialSpeechBubbleDict = LoadJson<Data.ConversationData, int, Data.Conversation>("TutorialSpeechBubbleData").MakeDict();

        CharacterInfoDict = LoadJson<Data.CharacterInfoData, int, Data.CharacterInfo>("CharacterInfoData").MakeDict();
        TutorialInfoDict = LoadJson<Data.TutorialInfoData, Define.TutorialType, Data.TutorialInfo>("TutorialInfoData").MakeDict();
        EnemyDict = LoadJson<Data.EnemyData, int, Data.Enemy>("EnemyData").MakeDict();
        StageEnemyDict = LoadJson<Data.StageEnemyData, Define.Scene, Data.StageEnemy>("StageEnemyData").MakeDict();

        PlayerData = GameData.Load<PlayerData>();
        EnemyData = GameData.Load<EnemyData>();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    public void SaveAllData()
    {
        PlayerData.Save();
        EnemyData.Save();
    }
    public void ClearAllData()
    {
        PlayerData.Clear();
        EnemyData.Clear();
    }
}
