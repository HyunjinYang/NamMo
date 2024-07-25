using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_WaveDetect : GameAbility
{
    [SerializeField] private float _originWaveSize;
    [SerializeField] private float _originWaveStrength;
    [SerializeField] private float _originOutlinePowerValue;
    [SerializeField] private float _scaleChangeValue;
    [SerializeField] private float _scaleChangeTime;
    [SerializeField] private Material _waveMaterial;
    [SerializeField] private float _detectMoment;
    public Action OnWaveStart;
    public Action OnWaveEnd;
    protected override void Init()
    {
        base.Init();
        _waveMaterial.SetFloat("_Size", _originWaveSize);
        _waveMaterial.SetFloat("_WaveStrength", _originWaveStrength);
        _waveMaterial.SetFloat("_OutlinePowerValue", _originOutlinePowerValue);
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
        //if (_asc.gameObject.GetComponent<PlayerMovement>().IsJumping || _asc.gameObject.GetComponent<PlayerMovement>().IsDashing) return false;
        return true;
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        if (OnWaveEnd != null) OnWaveEnd.Invoke();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
    IEnumerator CoWaveDetect()
    {
        yield return new WaitForSeconds(_detectMoment);
        for (int i = 0; i < (int)(_scaleChangeTime * 50); i++)
        {
            _waveMaterial.SetFloat("_Size", _originWaveSize + Mathf.Sin((90f / (100f * _scaleChangeTime)) * (i + 1) * Mathf.Deg2Rad) * _scaleChangeValue);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = (int)(_scaleChangeTime * 50); i > 0; i--)
        {
            _waveMaterial.SetFloat("_Size", _originWaveSize + Mathf.Sin((90f / (100f * _scaleChangeTime)) * (i - 1) * Mathf.Deg2Rad) * _scaleChangeValue);
            yield return new WaitForSeconds(0.02f);
        }
        EndAbility();
    }
}
