using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMovingPlatform : BaseInteractable
{
    [SerializeField] private TriggerMovingPlatform _interactable1;
    [SerializeField] private TriggerMovingPlatform _interactable2;
    [SerializeField] MovingPlatform _movingPlatform;
    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        _interactable1.ConnectToMovingPlatform();
        _interactable2.ConnectToMovingPlatform();
        _movingPlatform.MovePlatform();
        gameObject.SetActive(false);
    }
}
