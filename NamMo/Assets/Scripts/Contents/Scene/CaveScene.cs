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

        GameObject player = SpawnPlayer();

        PlayerData playerData = Managers.Data.PlayerData;
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

        

    }
    public override void Clear()
    {
    }
}
