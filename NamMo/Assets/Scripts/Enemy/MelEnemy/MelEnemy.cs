using System;
using System.Collections;
using System.Collections.Generic;
using NamMo;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

namespace Enemy.MelEnemy
{
    public class MelEnemy : Enemy
    {
        public Action OnDownAttack;
        public Action OnEndDownAttack;

        [SerializeField] public bool _isAttacking;

        public bool IsAttacking
        {
            get
            {
                return _isAttacking;
            }
        }
        
        public StateMachine stateMachine;
        
        [SerializeField] public EnemyBlockArea _enemyAttack1BlockArea;
        [SerializeField] public EnemyBlockArea _enemyAttack2BlockArea;
        [SerializeField] public EnemyBlockArea _enemyAttack3BlockArea;
        [SerializeField] private List<MelEnemyAttackPattern<MelEnemy>> _patternlist = new List<MelEnemyAttackPattern<MelEnemy>>();
        private Animator _animator;
        private MelEnemyAttackPattern<MelEnemy> _pattern;
        private Random _rand = new Random();

        private Coroutine _attackCoroutine;
        
        public float Attack1Time1;
        public float Attack1Time2;
        public float Attack2Time;
        public float _distance;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<MelEnemy>.Initialise(_animator, this);
            
            stateMachine = new StateMachine(this);
            stateMachine.Initialize(stateMachine._IdelState);
        }

        public void GroggyEnter()
        {
            _enemyAttack1BlockArea._groggy += OnGroggy;
            _enemyAttack2BlockArea._groggy += OnGroggy;
            _enemyAttack3BlockArea._groggy += OnGroggy;
        }

        public void GroggyExit()
        {
            _enemyAttack1BlockArea._groggy -= OnGroggy;
            _enemyAttack2BlockArea._groggy -= OnGroggy;
            _enemyAttack3BlockArea._groggy -= OnGroggy;   
        }

        public override void Behavire(float distance)
        {
            stateMachine.Update();
            _distance = distance;
        }
        
        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }

        public void Attack()
        {
            _enemyMovement._isAttack = true;
            _enemyMovement.OnWalk(0f);
            _isAttacking = false;
            _attackCoroutine = StartCoroutine(CoAttack());
        }

        public void EndAttack()
        {
            _enemyMovement._isAttack = false;
            _isAttacking = false;
            StopCoroutine(_attackCoroutine);
            _pattern = null;
        }
        
        public void Patrol()
        {
            _enemyMovement._isPatrol = true;
        }

        public void EndPatrol()
        {
            _enemyMovement._isPatrol = false;
        }

        public void Groggy()
        {
            _enemyMovement._isGroggy = true;
        }

        public void EndGroggy()
        {
            _enemyMovement._isGroggy = false;
        }

        public void TransitionGroggy()
        {
            stateMachine.TransitionState(stateMachine._GroggyState);
        }

        public void TransitionEndGroggy()
        {
            stateMachine.TransitionState(stateMachine._patrolstate);
        }

        public void TransitionHit()
        {
            stateMachine.TransitionState(stateMachine._HitState);
        }

        public void TransitionEndHit()
        {
            stateMachine.TransitionState(stateMachine._patrolstate);
        }
        
        private IEnumerator CoAttack()
        {
            while (true)
            {
                var next = _rand.Next(0, 2);

                _pattern = _patternlist[next];
                _pattern.Initialise(this);
                _isAttacking = true;
                Debug.Log("sTART!");
                yield return StartCoroutine(_pattern.Pattern());
                _enemyAttack1BlockArea.DeActiveBlockArea();
                _enemyAttack2BlockArea.DeActiveBlockArea();
                _enemyAttack3BlockArea.DeActiveBlockArea();
                yield return new WaitForSeconds(2f);
            }
        }

    }
}