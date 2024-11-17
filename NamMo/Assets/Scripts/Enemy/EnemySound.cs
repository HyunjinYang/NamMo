using UnityEngine;

namespace Enemy.MelEnemy
{
    public class EnemySound: SoundPlayer
    {
        [Header("공격 사운드")] 
        [SerializeField] private AudioClip _attackSound;

        [Header("피격 사운드")] 
        [SerializeField] private AudioClip _hittedSound;

        [Header("시그니처 사운드")] 
        [SerializeField] private AudioClip _signatureSound;
        protected override void Init()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAttackSound()
        {
            if (_attackSound == null)
                return;
            _audioSource.PlayOneShot(_attackSound);
        }

        public void PlayHitSound()
        {
            if (_hittedSound == null)
                return;
            _audioSource.PlayOneShot(_hittedSound);
        }

        public void PlaySignatureSound()
        {
            if (_signatureSound == null)
                return;
            _audioSource.PlayOneShot(_signatureSound);
        }
    }
}