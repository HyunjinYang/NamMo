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
    [Header("Remain Use Count")]
    [SerializeField] private int _remainUseCnt;

    [Header("Wave Detect Light")]
    [SerializeField] private GameObject _waveDetectLight;
    [SerializeField] private float _lightRemainingTime;
    [SerializeField] private float _reviseValue;
    [SerializeField] private float _detectLightIntencity;

    [Header("VFX")]
    private GameObject _waveEffect;

    private Coroutine _turnOffLightCoroutine = null;
    private Coroutine _sizeUpWaveCoroutine = null;

    private List<DummyWaveEnemy> waveEnemies = new List<DummyWaveEnemy>();

    public Action<int> OnRemainUseCntChanged;
    public int RemainUseCnt 
    { 
        get 
        {  
            return _remainUseCnt; 
        }
        set
        {
            _remainUseCnt = value;
            if (OnRemainUseCntChanged != null) OnRemainUseCntChanged.Invoke(_remainUseCnt);
        }
    }
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
        //_remainUseCnt--;
        if (OnRemainUseCntChanged != null) OnRemainUseCntChanged.Invoke(_remainUseCnt);
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;

        GroundMaterialValueChanger.EndFadeOut();
        RefreshLight();
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_asc.gameObject.GetComponent<PlayerMovement>().IsGround == false) return false;
        if (_remainUseCnt <= 0) return false;
        return true;
    }
    protected override void CancelAbility()
    {
        base.CancelAbility();
        EndAbility();
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
    }
    private void HandleTriggeredWaveObject(GameObject go)
    {
        // tmp
        if (go.gameObject.GetComponent<EnemyWave>())
        {
            go.gameObject.GetComponent<EnemyWave>().Parried();
            SetWaveColor(Color.red, 4f);
        }
        if (go.gameObject.GetComponent<DummyWaveEnemy>())
        {
            DummyWaveEnemy enemy = go.gameObject.GetComponent<DummyWaveEnemy>();
            enemy.InPlayerWaveDetect = true;
            waveEnemies.Add(enemy);
        }
    }
    private void ShowWaveVFX()
    {
        _waveEffect = Instantiate(_waveDetectEffectPrefab, _asc.GetPlayerController().transform.position, Quaternion.identity);
        _waveEffect.GetComponent<Renderer>().sortingOrder = 1;
        _waveEffect.GetComponent<VFXController>().Play(_scaleChangeTime);
    }
    public void SetWaveColor(Color color, float intencity)
    {
        if (_waveEffect)
        {
            _waveEffect.GetComponent<VFXController>().SetColor(color, intencity);
        }
    }
    private void ClearInWaveEnemies()
    {
        foreach (DummyWaveEnemy waveEnemy in waveEnemies)
        {
            if (waveEnemy)
            {
                waveEnemy.InPlayerWaveDetect = false;
            }
        }
    }
    private void RefreshLight()
    {
        _waveDetectLight.GetComponent<Light2D>().intensity = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
    }
    IEnumerator CoWaveDetect()
    {
        _waveDetectLight.transform.position = _asc.transform.position;
        yield return new WaitForSecondsRealtime(_detectMoment);

        //Managers.Sound.Play("Wave");
        _asc.GetPlayerController().GetPlayerSound().PlayWaveSound();
        _asc.GetPlayerController().GetWaveTrigger().transform.SetParent(null);
        _asc.GetPlayerController().GetWaveTrigger().transform.position = _asc.GetPlayerController().transform.position;
        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered -= HandleTriggeredWaveObject;
        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered += HandleTriggeredWaveObject;

        if (_turnOffLightCoroutine != null) StopCoroutine(_turnOffLightCoroutine);
        if (_sizeUpWaveCoroutine != null) StopCoroutine(_sizeUpWaveCoroutine);
        ClearInWaveEnemies();
        _waveDetectLight.GetComponent<Light2D>().intensity = _detectLightIntencity;
        _asc.GetPlayerController().GetWaveTrigger().SetRadius(0);

        ShowWaveVFX();

        _sizeUpWaveCoroutine = StartCoroutine(CoSizeUpWave());
        GroundMaterialValueChanger.FadeOutDistance = 0f;
        _turnOffLightCoroutine = StartCoroutine(CoTurnOffDetectLight());
    }
    
    IEnumerator CoTurnOffDetectLight()
    {
        yield return new WaitForSecondsRealtime(_scaleChangeTime + _lightRemainingTime);
        GroundMaterialValueChanger.StartFadeOut();
        yield return new WaitForSeconds(5f);
        GroundMaterialValueChanger.EndFadeOut();
        //float startTime = Time.time;
        //while (true)
        //{
        //    float currTime = Time.time;
        //    float remainTime = currTime - startTime;
        //    if (remainTime >= 2f) break;

        //    _waveDetectLight.GetComponent<Light2D>().intensity = (2f - remainTime) * _detectLightIntencity / 2f + 0.001f;

        //    yield return null;
        //}

        RefreshLight();
        _turnOffLightCoroutine = null;
    }
    IEnumerator CoSizeUpWave()
    {
        for (int i = 0; i < (int)(_scaleChangeTime * 25); i++)
        {
            float size = (25 * i) / (_scaleChangeTime * 25);
            size *= _reviseValue;
            _waveDetectLight.GetComponent<Light2D>().pointLightInnerRadius = size;
            _waveDetectLight.GetComponent<Light2D>().pointLightOuterRadius = size;
            _asc.GetPlayerController().GetWaveTrigger().SetRadius(size);
            yield return new WaitForSecondsRealtime(0.04f);
        }
        _asc.GetPlayerController().GetWaveTrigger().OnWaveRangeTriggerEntered -= HandleTriggeredWaveObject;
        ClearInWaveEnemies();
        
        waveEnemies.Clear();
        _sizeUpWaveCoroutine = null;
    }
    IEnumerator CoEndAbility()
    {
        yield return new WaitForSecondsRealtime(_motionTime);
        EndAbility();
    }
}
