using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : BaseInteractable
{
    [SerializeField] protected Model_InteractionTextData _stopTestProjectileTextData;
    [SerializeField] private bool _isActive;
    [SerializeField] private ProjectileSpawner _spawner;

    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        if (_isActive)
        {
            _spawner.StopSpawnProjectile();
            _isActive = false;
            _currentInteractionTextData = _originInteractionTextData;
        }
        else
        {
            _spawner.StartSpawnProjectile();
            _isActive = true;
            _currentInteractionTextData = _stopTestProjectileTextData;
        }
    }
}
