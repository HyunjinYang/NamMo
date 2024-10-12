using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScene : BaseScene
{
    [SerializeField] private Transform _spawnPos1;
    [SerializeField] private Transform _spawnPos2;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.TestStartScene;

        GameObject player = SpawnPlayer();

        PlayerData playerData = Managers.Data.PlayerData;
        if (PlayerData.Respawn)
        {
            PlayerData.Respawn = false;
            player.transform.position = playerData.Position;
        }
        else
        {
            if (playerData.isNewData)
            {
                player.transform.position = _spawnPos1.position;
                Managers.Data.SaveAllData();
                //playerData.Save();
            }
            else if (Managers.Scene.LastLocatedScene == Define.Scene.CaveScene
                || Managers.Scene.LastLocatedScene == Define.Scene.KatanaZeroScene1)
            {
                player.transform.position = _spawnPos2.position;
            }
            else
            {
                player.transform.position = playerData.Position;
            }
        }
    }
    public override void Clear()
    {
    }
}
