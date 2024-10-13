using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : SoundPlayer
{
    [Header("공격 사운드")]
    [SerializeField] private AudioClip _attackSound;
    [Header("피격 사운드")]
    [SerializeField] private AudioClip _hittedSound;
    [Header("대쉬 사운드")]
    [SerializeField] private AudioClip _dashSound;
    [Header("점프 사운드")]
    [SerializeField] private AudioClip _jumpSound;
    [Header("착지 사운드")]
    [SerializeField] private AudioClip _landSound;
    [Header("패링 사운드")]
    [SerializeField] private AudioClip _parryingSound;
    [Header("파동 사운드")]
    [SerializeField] private AudioClip _waveSound;

    private PlayerController _pc;
    protected override void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SetPlayerController(PlayerController pc)
    {
        _pc = pc;
    }
    public void PlayAttackSound()
    {
        if (_attackSound == null) return;
        _audioSource.PlayOneShot(_attackSound);
    }
    public void PlayHittedSound()
    {
        if (_hittedSound == null) return;
        _audioSource.PlayOneShot(_hittedSound);
    }
    public void PlayDashSound()
    {
        if (_dashSound == null) return;
        _audioSource.PlayOneShot(_dashSound);
    }
    public void PlayJumpSound()
    {
        if (_jumpSound == null) return;
        _audioSource.PlayOneShot(_jumpSound);
    }
    public void PlayLandSound()
    {
        if (_landSound == null) return;
        _audioSource.PlayOneShot(_landSound);
    }
    public void PlayParryingSound()
    {
        if (_parryingSound == null) return;
        _audioSource.PlayOneShot(_parryingSound);
    }
    public void PlayWaveSound()
    {
        if (_waveSound == null) return;
        _audioSource.PlayOneShot(_waveSound);
    }
}
