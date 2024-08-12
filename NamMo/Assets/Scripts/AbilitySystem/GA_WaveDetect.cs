using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GA_WaveDetect : GameAbility
{
    [SerializeField] private Material _waveMaterial;
    [SerializeField] private float _originWaveDistanceFromCenter;
    [SerializeField] private float _scaleChangeTime;
    [SerializeField] private float _detectMoment;
    [SerializeField] private float _motionTime;

    [SerializeField] private GameObject _waveDetectEffect;

    [Header("Wave Detect Light")]
    [SerializeField] private GameObject _waveDetectLight;
    [SerializeField] private float _lightRemainingTime;
    public Action OnWaveStart;
    public Action OnWaveEnd;

    private Coroutine _turnOffLightCoroutine = null;
    protected override void Init()
    {
        base.Init();
        _waveMaterial.SetFloat("_WaveDistanceFromCenter", _originWaveDistanceFromCenter);

        _waveDetectEffect.transform.SetParent(null);
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
    IEnumerator CoWaveDetect()
    {
        _waveDetectEffect.transform.position = _asc.transform.position;
        _waveDetectLight.transform.position = _asc.transform.position;
        yield return new WaitForSeconds(_detectMoment);
        if (_turnOffLightCoroutine != null) StopCoroutine(_turnOffLightCoroutine);
        _waveDetectLight.GetComponent<Light2D>().intensity = 0.1f;
        _waveMaterial.DOFloat(1, "_WaveDistanceFromCenter", _scaleChangeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            _waveMaterial.SetFloat("_WaveDistanceFromCenter", _originWaveDistanceFromCenter);
        });
        for (int i = 0; i < (int)(_scaleChangeTime * 50); i++)
        {
            float size = _originWaveDistanceFromCenter + (40 * i) / (_scaleChangeTime * 50);
            _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = size;
            _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = size;
            yield return new WaitForSeconds(0.02f);
        }
        _turnOffLightCoroutine = StartCoroutine(CoTurnOffDetectLight());
    }
    IEnumerator CoTurnOffDetectLight()
    {
        yield return new WaitForSeconds(_lightRemainingTime - 2f);
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
    IEnumerator CoEndAbility()
    {
        yield return new WaitForSeconds(_motionTime);
        EndAbility();
    }
}
