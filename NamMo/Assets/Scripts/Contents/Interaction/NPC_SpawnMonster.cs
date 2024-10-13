using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnEnemyInfo
{
    public int enemyId;
    public Transform transform;
}
public class NPC_SpawnMonster : BaseInteractable
{
    [SerializeField] private List<SpawnEnemyInfo> _spawnEnemies = new List<SpawnEnemyInfo>();
    private UI_Conversation _conversationUI;
    private bool _spawned = false;

    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        _conversationUI = Managers.UI.ShowUI<UI_Conversation>();
        _conversationUI.SetConversationType(Define.ConversationType.SpawnMonsterTest);
        Managers.Scene.CurrentScene.Player.BlockInput = true;
        if (_spawned)
        {
            _conversationUI.RegisterFlowAction(0, () =>
            {
                HideConversationUI();
            });
        }
        else
        {
            _spawned = true;
            _conversationUI.RegisterFlowAction(0, () =>
            {
                SpawnMonsters();
                HideConversationUI();
            });
        }
    }
    private void SpawnMonsters()
    {
        foreach (var enemyInfo in _spawnEnemies)
        {
            string enemyPrefabPath = Managers.Data.EnemyDict[enemyInfo.enemyId].prefabPath;
            GameObject enemy = Managers.Resource.Instantiate(enemyPrefabPath);
            enemy.transform.position = enemyInfo.transform.position;
        }
    }
    private void HideConversationUI()
    {
        Managers.Scene.CurrentScene.Player.BlockInput = false;
        Managers.Resource.Destroy(_conversationUI.gameObject);
    }
}
