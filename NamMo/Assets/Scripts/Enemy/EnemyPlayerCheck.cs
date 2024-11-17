using System;
using UnityEditor;
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

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _enemy._distance = Int64.MaxValue;
            }
        }


        private void AlertNearbyEnemies()
        {
            if (isAlert)
                return;
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(_enemy.gameObject.transform.position, 10.5f, _layerMask);

            Debug.Log(nearbyEnemies.Length);
            isAlert = true;
            
            

            foreach (var enemy in nearbyEnemies)
            {
                Enemy _enemys = enemy.GetComponent<Enemy>();
                if (_enemys != null)
                {
                    Debug.Log(_enemys.gameObject.name);
                    _enemys.PlayerTrackingState();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_enemy.transform.position, 10.5f);
        }

        public void FindPlayer()
        {
            _enemy.PlayerTrackingState();
        }
    }
}