using System;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class PlayerCheck: MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        private Collider2D _hitCollider;
        [SerializeField] private TestEnemy _enemy;

        private bool isAlert = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _enemy._isPlayerCheck = true;
                AlertNearbyEnemies();
            }
        }


        private void AlertNearbyEnemies()
        {
            if (isAlert)
                return;
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(_enemy.gameObject.transform.position, 10.5f, _layerMask);
            
            isAlert = true;
            
            

            foreach (var enemy in nearbyEnemies)
            {
                TestEnemy _enemys = enemy.GetComponent<TestEnemy>();
                if (_enemys != null)
                {
                    Debug.Log(_enemys.gameObject.name);
                    _enemys._isPlayerCheck = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_enemy.transform.position, 10.5f);
        }
    }
}