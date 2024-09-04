using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private BaseScene _baseScene = null;
    public Define.Scene LastLocatedScene;
    public BaseScene CurrentScene
    {
        get
        {
            if(_baseScene == null)
            {
                _baseScene = GameObject.FindObjectOfType<BaseScene>();
            }
            return _baseScene;
        }
    }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        CurrentScene.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        LastLocatedScene = CurrentScene.SceneType;
        CurrentScene.Clear();
        _baseScene = null;
    }
}
