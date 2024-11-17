﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents
    TimeManager _time = new TimeManager();
	#endregion

	#region Core
	DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    InputManager _input = new InputManager();
    EffectManager _effect = new EffectManager();
    private GameManager _gameManage = new GameManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static InputManager Input { get { return Instance._input; } }
    public static GameManager Gamemanager { get { return Instance._gameManage; } }
    public static TimeManager Time { get { return Instance._time; } }
    public static EffectManager Effect { get { return Instance._effect; } }
	#endregion

	void Start()
    {
        Init();
	}

    void Update()
    {
    }

    static void Init()
    {
        if (s_instance == null)
        {
			GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._input = new InputManager();
            s_instance._input.Init();
            s_instance._sound.Init();
            s_instance._time.Init();
            s_instance._effect.Init();
            //s_instance._gameManage.Init();
        }		
	}

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
        Time.Clear();
    }
    public static IEnumerator CoDelayAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
