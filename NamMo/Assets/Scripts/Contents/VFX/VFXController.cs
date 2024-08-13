using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    private VisualEffect _vfx;
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        _vfx.Stop();
    }
    public void Play(float lifeTime)
    {
        _vfx.SetFloat("LifeTime", lifeTime);
        _vfx.Play();
        Destroy(gameObject, lifeTime);
    }
}
