using System;
using Enemy.Boss.MiniBoss;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int _hp;
        protected GameObject _player;
        [SerializeField] protected EnemyMovement _enemyMovement;
        public Action Onattack;
        public Action OnEndattack;
        public Action OnHit;
        public Action OnEndHit;
        public Action Dead;
        public Action OnGroggy;
        public Action OnEndGroggy;

        public float maxGroggyStet;
        public float currentgroggyStet;
        
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

            if (_hp <= 0)
            {
                OnDead();
                return;
            }
            
            if(gameObject.GetComponent<MiniBossEnemy>() == null)
                OnHit.Invoke();
            _enemyMovement._isHit = true;
        }

        public void OnDead()
        {
            _enemyMovement._isDead = true;
            Dead.Invoke();
        }

        public bool ReturnIsHit()
        {
            return _enemyMovement._isHit;
        }

        public bool ReturnIsDead()
        {
            return _enemyMovement._isDead;
        }
        public void EndHit()
        {
            _enemyMovement._isHit = false;
            OnEndHit.Invoke();
        }

        public virtual void GroggyStetCount()
        {
            
        }

        public virtual void GroggyEnter()
        {
            
        }
        
    }
}