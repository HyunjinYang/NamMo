using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BaseInteractable
{
    [Header("Target Position")]
    [SerializeField] private Transform _targetPos;

    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        _player.transform.position = _targetPos.position;
    }
}
