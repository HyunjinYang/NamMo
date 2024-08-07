using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        protected int _hp;
        protected GameObject _player;
        [SerializeField] protected EnemyMovement _enemyMovement;
        public Action Onattack;
        public Action OnEndattack;
        public Action OnHit;
        public Action OnEndHit;
        public bool _isParingAvailable = false;
        public virtual void Behavire(float distance)
        {
            
        }

        public void PlayerFind(GameObject player)
        {
            _player = player;
            _enemyMovement._playerposition = _player.transform;
        }

        public void PlayerExit()
        {
            _player = null;
            _enemyMovement = null;
        }

        public void Hit(int damage)
        {
            _hp -= damage;
            _enemyMovement._isHit = true;
            OnHit.Invoke();
        }

        public void EndHit()
        {
            _enemyMovement._isHit = false;
            OnEndHit.Invoke();
        }
    }
}