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
    public void Init()
    {
        PrologueFadeInScriptDict = LoadJson<Data.PrologueFadeInScriptData, int, Data.PrologueFadeInScript>("PrologueFadeInScriptData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
