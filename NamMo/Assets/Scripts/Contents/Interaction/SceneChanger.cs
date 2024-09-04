using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : BaseInteractable
{
    [SerializeField] private Define.Scene _targetScene;
    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        Managers.Data.PlayerData.RefreshData();
        Managers.Scene.LoadScene(_targetScene);
    }
}
