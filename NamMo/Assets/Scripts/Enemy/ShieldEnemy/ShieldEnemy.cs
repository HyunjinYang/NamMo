using System;
using DG.Tweening;
using Enemy.ShieldEnemy.State;
using UnityEngine;

namespace Enemy.ShieldEnemy
{
    public class ShieldEnemy: Enemy
    {

        public bool _isAttacking;

        public ShieldEnemyStateMachine _statemachine;
        public EnemyAttackArea _EnemyAttackArea;

        public bool _isBlocking = true;
        
        private Animator _animator;
        protected override void Start()
        {
            base.Start();

            _animator = GetComponent<Animator>();

            _statemachine = new ShieldEnemyStateMachine(this);
            _statemachine.Initialize(_statemachine._IdelState);

            _distance = Int64.MaxValue;
            
            _EnemyAttackArea.SetAttackInfo(gameObject, 1);
        }

        public override void GroggyEnter()
        {
            
        }
        public override void Behavire(float distance)
        {
            base.Behavire(distance);
            
            
            _distance = distance;
        }

        public void Update()
        {
            _statemachine.Update();
        }

        #region Animation

        public void AttackAnim()
        {
            Onattack.Invoke();
        }

        public void EndAttackAnim()
        {
            OnEndattack.Invoke();
        }
        #endregion

        #region Pattern

        public void Attack()
        {
            _enemyMovement._isAttack = true;
            _enemyMovement.OnWalk(0F);
            
        }
        #endregion

        #region State
        
        #endregion
        
    }
}