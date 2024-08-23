using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWaveEnemyController : BaseInteractable
{
    [SerializeField] private Model_InteractionTextData _stopShootEnemyWaveTextData;
    [SerializeField] private DummyWaveEnemy _waveEnemy;
    [SerializeField] private bool _isActive;

    [Range(3, 10)]
    [SerializeField] float _shootWavePeriod;

    private Coroutine _shootEnemyWaveCoroutine = null;

    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        if (_isActive)
        {
            _isActive = false;
            _currentInteractionTextData = _originInteractionTextData;

            StopCoroutine(_shootEnemyWaveCoroutine);
        }
        else
        {
            _isActive = true;
            _currentInteractionTextData = _stopShootEnemyWaveTextData;

            _shootEnemyWaveCoroutine = StartCoroutine(CoShootEnemyWave());
        }
    }

    IEnumerator CoShootEnemyWave()
    {
        while (true)
        {
            _waveEnemy.ShootWave();
            yield return new WaitForSeconds(_shootWavePeriod);
        }
    }
}
