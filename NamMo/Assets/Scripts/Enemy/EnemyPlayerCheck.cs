using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyPlayerCheck: MonoBehaviour
    {
        private readonly Collider2D[] ResultObj = new Collider2D[10];
        private CircleCollider2D _collider;
        private ContactFilter2D _CheckObject;
        [SerializeField] private LayerMask _layerMask;
        private Collider2D _hitCollider;
        [SerializeField] private Enemy _enemy;
        private void Awake()
        {
            _CheckObject.layerMask = _layerMask;
            _CheckObject.useLayerMask = true;
            _collider = GetComponent<CircleCollider2D>();
        }

        private void FixedUpdate()
        {
            //PlayerCheck();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _enemy.PlayerFind(other.gameObject);
                PlayerCheck();
            }
        }


        private void PlayerCheck()
        {
            var hitCount = Physics2D.OverlapCircle(transform.position, 7.5f, _CheckObject, ResultObj);
            for (var i = 0; i < hitCount; i++)
            {
                _hitCollider = ResultObj[i];
                float distance = Vector2.Distance(_hitCollider.gameObject.transform.position, transform.position);
                _enemy.Behavire(distance);
            }
        }
    }
}