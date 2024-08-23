using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWaveEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyWavePrefab;

    private bool _stunned = false;
    private Coroutine _stunCoroutine = null;
    public bool InPlayerWaveDetect = false;
    public void ShootWave()
    {
        if (_stunned) return;
        if (InPlayerWaveDetect) return;
        GameObject go = Instantiate(_enemyWavePrefab, transform.position, Quaternion.identity);
        EnemyWave wave = go.GetComponent<EnemyWave>();
        wave.DoWave(this);
    }
    public void WaveParried()
    {
        if (_stunCoroutine != null)
        {
            StopCoroutine(_stunCoroutine);
        }
        _stunCoroutine = StartCoroutine(CoStun());
    }
    IEnumerator CoStun()
    {
        _stunned = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.transform.DOShakePosition(1f);
        yield return new WaitForSeconds(4f);
        _stunned = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        _stunCoroutine = null;
    }
}
