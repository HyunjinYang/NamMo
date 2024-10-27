using System;
using System.Collections;
using System.Collections.Generic;
using NamMo;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Animator = UnityEngine.Animator;
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
        
        [SerializeField] public EnemyAttackArea EnemyAttack1AttackArea;
        [SerializeField] public EnemyAttackArea EnemyAttack2AttackArea;
        [SerializeField] public EnemyAttackArea EnemyAttack3AttackArea;
        [SerializeField] private List<MelEnemyAttackPattern<MelEnemy>> _patternlist = new List<MelEnemyAttackPattern<MelEnemy>>();
        
        private Animator _animator;
        private MelEnemyAttackPattern<MelEnemy> _pattern;
        private Random _rand = new Random();

        private Coroutine _attackCoroutine;
        private Coroutine _currentPattern;
        private Coroutine _turmCoroutine;

        public bool isTest = false;
        public bool Test = false;
        public float Attack1Time1;
        public float Attack1Time2;
        public float Attack2Time;
        public float _distance;
        protected override void Start()
        {
            base.Start();
            
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<MelEnemy>.Initialise(_animator, this);
            
            stateMachine = new StateMachine(this);
            if(Test)
                stateMachine.Initialize(stateMachine._patrolstate);
            else
                stateMachine.Initialize(stateMachine._IdelState);
            _distance = Int64.MaxValue;
            EnemyAttack1AttackArea.SetAttackInfo(gameObject, 1);
            EnemyAttack2AttackArea.SetAttackInfo(gameObject, 1);
            EnemyAttack3AttackArea.SetAttackInfo(gameObject, 1);
        }

        public override void GroggyEnter()
        {
            EnemyAttack1AttackArea._groggy += OnGroggy;
            EnemyAttack2AttackArea._groggy += OnGroggy;
            EnemyAttack3AttackArea._groggy += OnGroggy;
        }

        public void GroggyExit()
        {
            EnemyAttack1AttackArea._groggy -= OnGroggy;
            EnemyAttack2AttackArea._groggy -= OnGroggy;
            EnemyAttack3AttackArea._groggy -= OnGroggy;   
        }

        public override void Behavire(float distance)
        {
            //stateMachine.Update();
            _distance = distance;
        }

        public void Update()
        {
            stateMachine.Update();
        }

        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }
        
        public void Attack()
        {
            _enemyMovement._isAttack = true;
            _enemyMovement.OnWalk(0f);
            _attackCoroutine = StartCoroutine(CoAttack());
        }

        public void EndAttack()
        {
            _enemyMovement._isAttack = false;
            _isAttacking = false;
            OnEndattack.Invoke();
            OnEndDownAttack.Invoke();
            if(_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
            if(_currentPattern != null)
                StopCoroutine(_currentPattern);
            if(_turmCoroutine != null)
                StopCoroutine(_turmCoroutine);
            _pattern = null;
        }
        
        public void Direct()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
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
            var next = _rand.Next(0, 2);

            _pattern = _patternlist[1];
            _pattern.Initialise(this);
            _isAttacking = true;
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            yield return _currentPattern = StartCoroutine(_pattern.Pattern());
            
            stateMachine.TransitionState(stateMachine._TurmState);
        }

        public override IEnumerator CoTurm()
        {
            yield return new WaitForSeconds(1.5f);
            stateMachine.TransitionState(stateMachine._patrolstate);
        }

    }
}