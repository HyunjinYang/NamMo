using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData : GameData
{
    public bool isNewData = true;
    public List<Data.StageEnemy> stageEnemies = new List<Data.StageEnemy>();
    protected override void Init(string fileName)
    {
        base.Init(fileName);
        if (isNewData)
        {
            for (int i = 0; i < (int)Define.Scene.MaxCount; i++)
            {
                stageEnemies.Add(null);
            }
            foreach(var v in Managers.Data.StageEnemyDict)
            {
                stageEnemies[(int)v.Key] = v.Value;
            }
            //foreach (Data.StageEnemy enemy in stageEnemies)
            //{
            //    if (enemy == null) continue;
            //    foreach (Data.EachEnemyInfo e in enemy.enemies)
            //    {
            //        Debug.Log(e.alive);
            //    }
            //}
        }
    }
    public override void Save()
    {
        RefreshData();
        base.Save();
    }
    public override void RefreshData()
    {
        isNewData = false;
        // TODO
    }
    public override void Clear()
    {
        EnemyData data = new EnemyData();
        data.Init(typeof(EnemyData).Name);
        Managers.Data.EnemyData = data;
    }
    public void KillEnemy(Define.Scene scene, int managedId)
    {
        stageEnemies[(int)scene].enemies[managedId].alive = false;
    }
}
