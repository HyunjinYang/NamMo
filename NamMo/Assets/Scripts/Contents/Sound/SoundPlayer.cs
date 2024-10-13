using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;
    void Start()
    {
        Init();
    }
    protected virtual void Init() { }
    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
