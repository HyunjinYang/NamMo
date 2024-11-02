using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class BaseScene : MonoBehaviour
{
    
    private PlayerController _pc = null;
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    public PlayerController Player { get { return _pc; } }

	void Awake()
	{
		Init();
	}

	protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();

    public void LoadScene(string sceneName)
    {
        //StartCoroutine(LoadSceneAsync(sceneName));
        SceneManager.LoadScene(sceneName);
    }
    public void SetPlayerController(PlayerController pc)
    {
        _pc = pc;
    }
    protected GameObject SpawnPlayer()
    {
        // 플레이어 생성
        GameObject player = Managers.Resource.Instantiate("Nammo");

        // 플레이어 UI 생성
        UI_Hud hudUI = Managers.UI.ShowSceneUI<UI_Hud>();
        hudUI.Init();

        player.GetComponent<PlayerController>().SetPlayerInfoByPlayerData();

        //Camera.main.GetComponent<CameraController>().SetTargetInfo(player);
        Camera.main.GetComponent<CameraController>().CameraMode = Define.CameraMode.FollowTarget;

        return player;
    }
    protected void SpawnEnemies()
    {
        Dictionary<int, Data.Enemy> enemyDict = Managers.Data.EnemyDict;
        Data.StageEnemy stageEnemy = Managers.Data.EnemyData.stageEnemies[(int)SceneType];
        foreach (var enemy in stageEnemy.enemies)
        {
            if (enemy.alive == false) continue;
            string prefabPath = enemyDict[enemy.enemyId].prefabPath;
            GameObject go = Managers.Resource.Instantiate(prefabPath);
            go.transform.position = new Vector2(enemy.posX, enemy.posY);

            go.GetComponentInChildren<Enemy.Enemy>().ManagedId = enemy.managedId;
        }
    }

    //IEnumerator LoadSceneAsync(string sceneName)
    //{
    //    yield return null;

    //    AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
    //    UI_Loading ui_Lodding = Managers.UI.ShowLoadingUI<UI_Loading>();

    //    op.allowSceneActivation = false;

    //    float timer = 0.0f;
    //    while (!op.isDone)
    //    {
    //        yield return null;
    //        timer += Time.deltaTime;
    //        if (op.progress < 0.9f)
    //        {
    //            ui_Lodding.SetProgressBarValue(Mathf.Lerp(ui_Lodding.GetProgressBarValue(), op.progress, timer));
    //            if (ui_Lodding.GetProgressBarValue() >= op.progress)
    //            {
    //                timer = 0f;
    //            }
    //        }
    //        else
    //        {
    //            ui_Lodding.SetProgressBarValue(Mathf.Lerp(ui_Lodding.GetProgressBarValue(), 1f, timer));
    //            if (ui_Lodding.GetProgressBarValue() == 1.0f)
    //            {
    //                op.allowSceneActivation = true;
    //                yield break;
    //            }
    //        }
    //    }
    //}
}
