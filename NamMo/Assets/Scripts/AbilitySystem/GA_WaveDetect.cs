using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class GA_WaveDetect : GameAbility
{
    [SerializeField] private float _scaleChangeTime;
    [SerializeField] private float _detectMoment;
    [SerializeField] private float _motionTime;

    [SerializeField] private GameObject _waveDetectEffectPrefab;

    [Header("Wave Detect Light")]
    [SerializeField] private GameObject _waveDetectLight;
    [SerializeField] private float _lightRemainingTime;
    [SerializeField] private float _reviseValue;
    public Action OnWaveStart;
    public Action OnWaveEnd;

    private Coroutine _turnOffLightCoroutine = null;
    private Coroutine _sizeUpWaveCoroutine = null;
    protected override void Init()
    {
        base.Init();

        _waveDetectLight.transform.SetParent(null);
        _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        StartCoroutine(CoWaveDetect());
        StartCoroutine(CoEndAbility());
        if (OnWaveStart != null) OnWaveStart.Invoke();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
    }
    protected override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_asc.gameObject.GetComponent<PlayerMovement>().IsGround() == false) return false;
        return true;
    }
    public override void CancelAbility()
    {
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        if (OnWaveEnd != null) OnWaveEnd.Invoke();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
    private void HandleTriggeredWaveObject(GameObject go)
    {
        // TODO : 파동탐지 감지 이벤트
        Debug.Log($"파동 감지 : {go.name}");
    }
    IEnumerator CoWaveDetect()
    {
        _waveDetectLight.transform.position = _asc.transform.position;
        yield return new WaitForSeconds(_detectMoment);

        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered -= HandleTriggeredWaveObject;
        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered += HandleTriggeredWaveObject;

        if (_turnOffLightCoroutine != null) StopCoroutine(_turnOffLightCoroutine);
        if (_sizeUpWaveCoroutine != null) StopCoroutine(_sizeUpWaveCoroutine);
        _waveDetectLight.GetComponent<Light2D>().intensity = 0.1f;
        _asc.GetPlayerController().GetWaveTrigger().SetRadius(0);

        GameObject waveEffect = Instantiate(_waveDetectEffectPrefab, _asc.GetPlayerController().transform.position, Quaternion.identity);
        waveEffect.GetComponent<VFXController>().Play(_scaleChangeTime);

        _sizeUpWaveCoroutine = StartCoroutine(CoSizeUpWave());
        _turnOffLightCoroutine = StartCoroutine(CoTurnOffDetectLight());
    }
    IEnumerator CoTurnOffDetectLight()
    {
        yield return new WaitForSeconds(_scaleChangeTime + _lightRemainingTime - 2f);
        for(int i = 0; i < 5; i++)
        {
            _waveDetectLight.GetComponent<Light2D>().intensity = 0f;
            yield return new WaitForSeconds(0.2f);
            _waveDetectLight.GetComponent<Light2D>().intensity = 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
        _turnOffLightCoroutine = null;
    }
    IEnumerator CoSizeUpWave()
    {
        for (int i = 0; i < (int)(_scaleChangeTime * 50); i++)
        {
            float size = (50 * i) / (_scaleChangeTime * 50);
            size += _reviseValue;
            _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = size;
            _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = size;
            _asc.GetPlayerController().GetWaveTrigger().SetRadius(size);
            yield return new WaitForSeconds(0.02f);
        }
        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered -= HandleTriggeredWaveObject;
        _sizeUpWaveCoroutine = null;
    }
    IEnumerator CoEndAbility()
    {
        yield return new WaitForSeconds(_motionTime);
        EndAbility();
    }
}
