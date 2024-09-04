using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScene : BaseScene
{
    [SerializeField] private Transform _spawnPos1;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.TestStartScene;

        // 플레이어 생성
        GameObject player = Managers.Resource.Instantiate("Nammo");
        PlayerData playerData = Managers.Data.PlayerData;

        // 플레이어 UI 생성
        UI_Hud hudUI = Managers.UI.ShowSceneUI<UI_Hud>();
        hudUI.Init();

        player.GetComponent<PlayerController>().SetPlayerInfoByPlayerData();
        if (playerData.isNewData)
        {
            player.transform.position = _spawnPos1.position;
            playerData.Save();
        }
        else
        {
            player.transform.position = playerData.Position;
        }
        

    }
    public override void Clear()
    {
    }
}
