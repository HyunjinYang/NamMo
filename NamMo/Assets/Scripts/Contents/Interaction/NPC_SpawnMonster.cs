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
    private UI_YesOrNo _yesOrNoUI;
    private bool _spawned = false;

    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        if (_spawned) return; // tmp
        _conversationUI = Managers.UI.ShowUI<UI_Conversation>();
        _conversationUI.SetConversationType(Define.ConversationType.SpawnMonsterTest);
        Managers.Scene.CurrentScene.Player.BlockInput = true;
        if (_spawned)
        {
        }
        else
        {
            _conversationUI.RegisterFlowAction(1, () =>
            {
                _conversationUI.BlockInput = true;
                _yesOrNoUI = Managers.UI.ShowUI<UI_YesOrNo>();
                _yesOrNoUI.Init();
                _yesOrNoUI.SetRequestText("도와줄까?");
                _yesOrNoUI.AssignYesButtonEvent(() =>
                {
                    _conversationUI.ShowNextInfos(100);
                    Managers.Resource.Destroy(_yesOrNoUI.gameObject);
                    _conversationUI.BlockInput = false;
                });
                _yesOrNoUI.AssignNoButtonEvent(() =>
                {
                    _conversationUI.ShowNextInfos(200);
                    Managers.Resource.Destroy(_yesOrNoUI.gameObject);
                    _conversationUI.BlockInput = false;
                });
            });
            _conversationUI.RegisterFlowAction(101, () =>
            {
                _spawned = true;
                _collider.enabled = false;
                SpawnMonsters();
                HideConversationUI();
            });
            _conversationUI.RegisterFlowAction(200, () =>
            {
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
