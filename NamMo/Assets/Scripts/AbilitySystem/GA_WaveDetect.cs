using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GA_WaveDetect : GameAbility
{
    [SerializeField] private float _originWaveSize;
    [SerializeField] private float _originWaveStrength;
    [SerializeField] private float _originOutlinePowerValue;
    [SerializeField] private float _scaleChangeValue;
    [SerializeField] private float _scaleChangeTime;
    [SerializeField] private Material _waveMaterial;
    [SerializeField] private float _detectMoment;

    [Header("Wave Detect Light")]
    [SerializeField] private GameObject _waveDetectLight;
    [SerializeField] private float _lightRemainingTime;
    public Action OnWaveStart;
    public Action OnWaveEnd;

    private Coroutine _turnOffLightCoroutine = null;
    protected override void Init()
    {
        base.Init();
        _waveMaterial.SetFloat("_Size", _originWaveSize);
        _waveMaterial.SetFloat("_WaveStrength", _originWaveStrength);
        _waveMaterial.SetFloat("_OutlinePowerValue", _originOutlinePowerValue);

        _waveDetectLight.transform.SetParent(null);
        _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
    }
    protected override void ActivateAbility()
    {
        base.ActivateAbility();
        StartCoroutine(CoWaveDetect());
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
        _waveDetectLight.transform.position = _asc.transform.position;
        yield return new WaitForSeconds(_detectMoment);
        if (_turnOffLightCoroutine != null) StopCoroutine(_turnOffLightCoroutine);
        _waveDetectLight.GetComponent<Light2D>().intensity = 0.4f;
        for (int i = 0; i < (int)(_scaleChangeTime * 50); i++)
        {
            float size = _originWaveSize + Mathf.Sin((90f / (100f * _scaleChangeTime)) * (i + 1) * Mathf.Deg2Rad) * _scaleChangeValue;
            _waveMaterial.SetFloat("_Size", size);
            _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = size / 2 + _originWaveStrength / 2;
            _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = size / 2 + _originWaveStrength / 2;
            yield return new WaitForSeconds(0.02f);
        }
        _turnOffLightCoroutine = StartCoroutine(CoTurnOffDetectLight());
        for (int i = (int)(_scaleChangeTime * 50); i > 0; i--)
        {
            float size = _originWaveSize + Mathf.Sin((90f / (100f * _scaleChangeTime)) * (i - 1) * Mathf.Deg2Rad) * _scaleChangeValue;
            _waveMaterial.SetFloat("_Size", size);
            yield return new WaitForSeconds(0.02f);
        }
        EndAbility();
    }
    IEnumerator CoTurnOffDetectLight()
    {
        yield return new WaitForSeconds(_lightRemainingTime - 2f);
        for(int i = 0; i < 5; i++)
        {
            _waveDetectLight.GetComponent<Light2D>().intensity = 0f;
            yield return new WaitForSeconds(0.2f);
            _waveDetectLight.GetComponent<Light2D>().intensity = 0.4f;
            yield return new WaitForSeconds(0.2f);
        }
        _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
        _turnOffLightCoroutine = null;
    }
}
