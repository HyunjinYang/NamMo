using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaveStone : BaseInteractable
{
    [Header("Contents")]
    [SerializeField] protected Model_InteractionTextData _deActiveWaveStoneTextData;
    [SerializeField] private bool _isActive;

    [Header("Wave")]
    [SerializeField] private float _repeatTime;
    [SerializeField] private float _waveLifeTime;
    [Range(0f, 1f)]
    [SerializeField] private float _waveSize;

    [Header("Light")]
    [SerializeField] private float _lightReviseValue;
    [SerializeField] private float _lightRemainTime;

    private Coroutine _shootWaveCoroutine = null;
    protected override void Init()
    {
        base.Init();
    }
    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        if (_isActive)
        {
            StopCoroutine(_shootWaveCoroutine);
            _shootWaveCoroutine = null;
            _isActive = false;

            _currentInteractionTextData = _originInteractionTextData;
        }
        else
        {
            _shootWaveCoroutine = StartCoroutine(CoShootWave());
            _isActive = true;

            _currentInteractionTextData = _deActiveWaveStoneTextData;
        }
    }
    IEnumerator CoShootWave()
    {
        while (true)
        {
            GameObject vfx = Managers.Resource.Instantiate("VFX/WaveEffect");
            vfx.transform.position = transform.position;
            vfx.GetComponent<VFXController>().Play(_waveLifeTime, _waveSize);
            StartCoroutine(CoCreateAndSizeUpLight());
            yield return new WaitForSeconds(_repeatTime);
        }
    }
    IEnumerator CoCreateAndSizeUpLight()
    {
        GameObject waveStoneLight = Managers.Resource.Instantiate("WaveStoneLight");
        waveStoneLight.transform.position =transform.position;
        for (int i = 0; i < (int)(_waveLifeTime * 50); i++)
        {
            float size = (50 * i) * _waveSize / (_waveLifeTime * 50) + _lightReviseValue;
            waveStoneLight.GetComponent<Light2D>().pointLightInnerRadius = size;
            waveStoneLight.GetComponent<Light2D>().pointLightOuterRadius = size;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(_lightRemainTime);

        waveStoneLight.GetComponent<Light2D>().pointLightInnerRadius = 0;
        waveStoneLight.GetComponent<Light2D>().pointLightOuterRadius = 0;
        Managers.Resource.Destroy(waveStoneLight);
    }
}
