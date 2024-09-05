using System.Collections;
using System.Collections.Generic;
using Enemy.WaveAttack;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyWave : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _waveCollider;
    [SerializeField] private float _damage;
    [SerializeField] private float _scaleChangeTime;
    [SerializeField] private float _reviseValue;

    [Header("VFX")]
    [SerializeField] private GameObject _waveDetectEffectPrefab;

    private bool _ignoreCollision = false;
    private GameObject _waveEffect;

    private IWaveAttacker _owner;
    private void Start()
    {
        _waveCollider = GetComponent<CircleCollider2D>();
        _waveCollider.isTrigger = true;
        Destroy(gameObject, _scaleChangeTime);
    }
    private void ShowWaveVFX()
    {
        _waveEffect = Instantiate(_waveDetectEffectPrefab, _owner.GetPosition().position, Quaternion.identity);
        _waveEffect.GetComponent<VFXController>().Play(_scaleChangeTime);
    }
    public void Parried()
    {
        _ignoreCollision = true;
        _owner.WaveParried();
        _waveEffect.GetComponent<VFXController>().DestroyWave(1f);
    }
    public void DoWave(IWaveAttacker owner)
    {
        _owner = owner;
        ShowWaveVFX();
        StartCoroutine(CoWave());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ignoreCollision) return;
        if (collision.gameObject.GetComponent<PlayerController>() == null) return;
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        pc.GetPlayerCombatComponent().GetDamaged(_damage, transform.position);
    }
    IEnumerator CoWave()
    {
        for (int i = 0; i < (int)(_scaleChangeTime * 25); i++)
        {
            float size = (25 * i) / (_scaleChangeTime * 25);
            size *= _reviseValue;
            _waveCollider.radius = size;
            yield return new WaitForSeconds(0.04f);
        }
    }
}
