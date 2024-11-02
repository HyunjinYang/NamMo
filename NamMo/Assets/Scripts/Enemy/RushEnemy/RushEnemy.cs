using System;
using System.Collections;
using Enemy.RushEnemy.State;
using NamMo;
using UnityEngine;

namespace Enemy.RushEnemy
{
    public class RushEnemy: Enemy
    {
        public Action OnJump;
        public Action OnEndJump;

        public bool _isAttacking;

        public RushEnemyStateMachine _statemachine;
        public EnemyAttackArea EnemyAttackArea;

        public RushEnemyRushAttackPattern _RushAttackPattern;
        private Animator _animator;

        private Coroutine _currentPattern;
        
        public float _jumpdistance;
        
        public float _distance;
        public float AttackTime;
        
        protected override void Start()
        {
            base.Start();

            _animator = GetComponent<Animator>();
            SceneLinkedSMB<RushEnemy>.Initialise(_animator, this);

            _statemachine = new RushEnemyStateMachine(this);
            _statemachine.Initialize(_statemachine._IdelState);

            _distance = Int64.MaxValue;
            
            EnemyAttackArea.SetAttackInfo(gameObject, 1);
        }

        public override void GroggyEnter()
        {
            EnemyAttackArea._groggy += OnGroggy;
        }

        public void GroggyExit()
        {
            EnemyAttackArea._groggy -= OnGroggy;
        }

        public override void Behavire(float distance)
        {
            _distance = distance;
        }

        public void Update()
        {
            _statemachine.Update();
        }

        #region Animation

        public void JumpAnim()
        {
            OnJump.Invoke();
        }

        public void EndJumpAnim()
        {
            OnEndJump.Invoke();
        }

        public void RushAttackAnim()
        {
            Onattack.Invoke();
        }

        public void EndAttackAnim()
        {
            OnEndattack.Invoke();
        }

        #endregion

        #region Pattern

        public void Jump()
        {
            
        }

        public void RushAttack()
        {
            _enemyMovement._isAttack = true;
            _enemyMovement.OnWalk(0f);
            _RushAttackPattern.Initalize(this);
            _isAttacking = true;
            _currentPattern = StartCoroutine(_RushAttackPattern.Pattern());
        }

        public void StopPattern()
        {
            _isAttacking = false;
            OnEndattack.Invoke();
            
            if(_currentPattern != null)
                StopCoroutine(_currentPattern);

        }
        
        public override IEnumerator CoTurm()
        {
            yield return new WaitForSeconds(1.5f);
            _statemachine.TransitionState(_statemachine._PatrolState);
        }

        #endregion


        #region StateTransition

        public void TransitionToGroggy()
        {
            _statemachine.TransitionState(_statemachine._GroggyState);
        }

        public void TransitionEndGroggy()
        {
            _statemachine.TransitionState(_statemachine._PatrolState);
        }

        public void TransitionToHit()
        {
            _statemachine.TransitionState(_statemachine._HitState);
        }

        public void TransitionEndHit()
        {
            _statemachine.TransitionState(_statemachine._PatrolState);
        }

        public override void PlayerTrackingState()
        {
            _statemachine.TransitionState(_statemachine._PatrolState);
        }

        #endregion

        public void Direction()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
        }
        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }
    }
}