using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        StartCoroutine(CoPlaySoundClip());
    }

    IEnumerator CoPlaySoundClip()
    {
        while (true)
        {
            _audio.PlayOneShot(_clip);
            yield return new WaitForSeconds(1f);
        }
    }
}
