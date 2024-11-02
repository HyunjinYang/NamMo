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

        private bool isAlert = false;
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
            if (other.gameObject.layer == 3)
            {
                AlertNearbyEnemies();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _enemy.PlayerFind(other.gameObject);
                float distance = Vector2.Distance(other.gameObject.transform.position, transform.position);
                _enemy.Behavire(distance);
            }
        }

        

        private void AlertNearbyEnemies()
        {
            var hitCount = Physics2D.OverlapCircle(transform.position, 10.5f, _CheckObject, ResultObj);
            isAlert = true;
            for (var i = 0; i < hitCount; i++)
            {
                _hitCollider = ResultObj[i];
                EnemyPlayerCheck _enemycheck = _hitCollider.GetComponent<EnemyPlayerCheck>();

                if (_enemycheck != null && !_enemycheck.isAlert)
                {
                    _enemycheck.FindPlayer();
                }
                
            }
            
            
        }

        public void FindPlayer()
        {
            _enemy.PlayerTrackingState();
        }
    }
}