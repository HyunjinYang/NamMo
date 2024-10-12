using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaZeroScene1 : BaseScene
{
    [SerializeField] private Transform _spawnPos1;
    [SerializeField] private Transform _spawnPos2;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.KatanaZeroScene1;

        GameObject player = SpawnPlayer();

        PlayerData playerData = Managers.Data.PlayerData;
        if (PlayerData.Respawn)
        {
            PlayerData.Respawn = false;
            player.transform.position = playerData.Position;
        }
        else
        {
            // 게임 플로우대로 이전 씬에서 들어왔을 경우 지정된 좌표로
            if (Managers.Scene.LastLocatedScene == Define.Scene.TestStartScene)
            {
                player.transform.position = _spawnPos1.position;
            }
            else if(Managers.Scene.LastLocatedScene == Define.Scene.KatanaZeroScene2)
            {
                player.transform.position = _spawnPos2.position;
            }
            else if (Managers.Scene.LastLocatedScene == Define.Scene.MainScene)
            {
                // 메인씬에서 이어하기를 통해 들어왔을 경우 저장되어있는 플레이어의 좌표로
                player.transform.position = playerData.Position;
            }
        }

        Dictionary<int, Data.Enemy> enemyDict = Managers.Data.EnemyDict;
        Data.StageEnemy stageEnemy = Managers.Data.EnemyData.stageEnemies[(int)SceneType];
        foreach(var enemy in stageEnemy.enemies)
        {
            if (enemy.alive == false) continue;
            string prefabPath = enemyDict[enemy.enemyId].prefabPath;
            GameObject go = Managers.Resource.Instantiate(prefabPath);
            go.transform.position = new Vector2(enemy.posX, enemy.posY);
        }
    }
    public override void Clear()
    {
    }
}
