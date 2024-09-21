using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyDamageFlash : MonoBehaviour
    {
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashtime = 0.25f;

        private Material _material;
        private SpriteRenderer _spriteRenderer;

        private Coroutine _damageCoroutine;
        
        public void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _material = _spriteRenderer.material;
            
        }

        public void CallDamageFlash()
        {
            _damageCoroutine = StartCoroutine(DamageFlash());
        }

        private IEnumerator DamageFlash()
        {
            _material.SetColor("_FlashColor", _flashColor);

            float currentFlashAmount = 0f;
            float elapsedtime = 0f;

            while (elapsedtime < _flashtime)
            {
                elapsedtime += Time.deltaTime;

                currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedtime / _flashtime));
                _material.SetFloat("_FlashAmount", currentFlashAmount);

                yield return null;
            }
        }
        
    }
}