using Enemy.Boss.MiniBoss;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackArea : CloseAttack, IParryingable
    {
        public bool _isHit = false;
        private bool _blockCheck = false;
        public Action _groggy;

        private BoxCollider2D _boxCollider;

        protected override void Init()
        {
            base.Init();
            //_boxCollider = _collider as BoxCollider2D;
            //_boxCollider.enabled = false;
        }

        public void Parried(GameObject attacker, GameObject target = null)
        {
            Debug.Log(gameObject.name);
            Enemy enemy = _attacker.GetComponent<Enemy>();
            if(enemy as MiniBossEnemy == null)
            {
                //enemy.Hit(1000);
            }
            _groggy.Invoke();
        }
    }
}