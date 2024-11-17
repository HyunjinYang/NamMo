using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    private VisualEffect _vfx;
    private Coroutine _destroyCoroutine = null;
    [SerializeField] private bool _isGround = false;
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        if(!_isGround)
            _vfx.Stop();
    }
    public void Play(float lifeTime, float size = 1)
    {
        _vfx.playRate = 1 / Time.timeScale;

        _vfx.SetFloat("LifeTime", lifeTime);
        _vfx.SetFloat("Size", size);
        _vfx.Play();
        _destroyCoroutine = StartCoroutine(CoReserveDestroy(lifeTime));

        Managers.Scene.CurrentScene.OnTimeScaleChanged += SetLifeTime;
    }
    public void SetColor(Color color, float intencity)
    {
        float factor = Mathf.Pow(2, intencity);
        Color hdrColor = new Color(color.r * factor, color.g * factor, color.b * factor, color.a);
        _vfx.SetVector4("WaveColor", hdrColor);
    }
    public void SetLifeTime(float timeScale)
    {
        _vfx.playRate = 1 / timeScale;
    }
    public void DestroyWave(float time)
    {
        StopCoroutine(_destroyCoroutine);
        Destroy(gameObject, time);
    }
    private void OnDestroy()
    {
        Managers.Scene.CurrentScene.OnTimeScaleChanged -= SetLifeTime;
    }
    IEnumerator CoReserveDestroy(float lifeTime)
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
