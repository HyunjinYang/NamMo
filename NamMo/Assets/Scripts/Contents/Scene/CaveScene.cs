using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene : BaseScene
{
    [SerializeField] private Transform _spawnPos1;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.CaveScene;

        // 플레이어 생성
        GameObject player = Managers.Resource.Instantiate("Nammo");
        PlayerData playerData = Managers.Data.PlayerData;

        // 플레이어 UI 생성
        UI_Hud hudUI = Managers.UI.ShowSceneUI<UI_Hud>();
        hudUI.Init();

        // 리스폰 되어야 할 경우는 저장되어있는 플레이어의 좌표로
        if (PlayerData.Respawn)
        {
            PlayerData.Respawn = false;
            player.transform.position = playerData.Position;
        }
        else
        {
            // 게임 플로우대로 이전 씬에서 들어왔을 경우 지정된 좌표로
            if (Managers.Scene.LastLocatedScene == Define.Scene.TestStartScene
            || Managers.Scene.LastLocatedScene == Define.Scene.Unknown)
            {
                player.transform.position = _spawnPos1.position;
            }
            else if(Managers.Scene.LastLocatedScene == Define.Scene.MainScene)
            {
                // 메인씬에서 이어하기를 통해 들어왔을 경우 저장되어있는 플레이어의 좌표로
                player.transform.position = playerData.Position;
            }
        }
        player.GetComponent<PlayerController>().SetPlayerInfoByPlayerData();

        Camera.main.GetComponent<CameraController>().SetTargetInfo(player);
        Camera.main.GetComponent<CameraController>().CameraMode = Define.CameraMode.FollowTarget;

    }
    public override void Clear()
    {
    }
}
