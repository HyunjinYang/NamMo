using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatform : BaseInteractable
{
    [SerializeField] MovingPlatform _movingPlatform;
    private Collider2D _col;
    protected override void Init()
    {
        base.Init();
        _col = GetComponent<Collider2D>();
        _col.enabled = false;
    }
    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        _movingPlatform.MovePlatform();
        _col.enabled = false;
    }
    public void ConnectToMovingPlatform()
    {
        _movingPlatform.OnReached += Activate;
        _movingPlatform.OnLeaved += DeActive;
    }
    private void Activate()
    {
        _col.enabled = true;
    }
    private void DeActive()
    {
        _col.enabled = false;
    }
}
