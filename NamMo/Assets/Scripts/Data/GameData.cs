using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class GameData
{
    protected string _fileName;
    protected virtual void Init(string fileName) { _fileName = $"/{fileName}.json"; }
    public virtual void Save()
    {
        string filePath = Application.persistentDataPath + _fileName;
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonData);
        Debug.Log($"Save Completed : {_fileName}");
    }
    public static T Load<T>() where T : GameData, new()
    {
        string fileName = typeof(T).Name;
        string filePath = $"{Application.persistentDataPath}/{fileName}.json";
        if (File.Exists(filePath))
        {
            Debug.Log($"Data Load Success : {fileName}");
            string jsonData = File.ReadAllText(filePath);
            T data = JsonUtility.FromJson<T>(jsonData);
            data.Init(fileName);
            return data;
        }
        else
        {
            Debug.Log($"New Data Created : {fileName}");
            T data = new T();
            data.Init(fileName);
            return data;
        }
    }
    public abstract void RefreshData();
    public abstract void Clear();
}
