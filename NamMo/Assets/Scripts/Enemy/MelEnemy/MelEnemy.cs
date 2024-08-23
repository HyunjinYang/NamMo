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
        enum State
        {
            Patrol,
            Attack,
            None
        }
        
        public Action OnDownAttack;
        public Action OnEndDownAttack;

        public int patternCount = 0;
        
        [SerializeField] private State _state = State.None;
        [SerializeField] public EnemyBlockArea _enemyAttack1BlockArea;
        [SerializeField] public EnemyBlockArea _enemyAttack2BlockArea;
        [SerializeField] public EnemyBlockArea _enemyAttack3BlockArea;
        [SerializeField] private List<MelEnemyAttackPattern<MelEnemy>> _patternlist = new List<MelEnemyAttackPattern<MelEnemy>>();
        private Animator _animator;
        private MelEnemyAttackPattern<MelEnemy> _pattern;
        private Random _rand = new Random();
        private Coroutine _currentPattern;
        [SerializeField] public bool _isTurm = false;
        public float Attack1Time1;
        public float Attack1Time2;
        public float Attack2Time;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<MelEnemy>.Initialise(_animator, this);
            if (OnGroggy == null)
                Debug.Log("ASDWW1");
            _enemyAttack1BlockArea._groggy = OnGroggy;
            _enemyAttack2BlockArea._groggy = OnGroggy;
            _enemyAttack3BlockArea._groggy = OnGroggy;
        }

        public override void Behavire(float distance)
        {
            if (distance >= 3f && _state == State.None)
            {
                Patrol();
                _state = State.Patrol;
                _enemyMovement._isAttack = false;
            }
            else if (distance < 3f)
            {
                if (_state == State.Attack || _isTurm)
                    return;
                
                _state = State.Attack;

                AttackInit();
                
                var next = _rand.Next(0, 2);
                
                SetPattern(next);
                _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
                StartPattern();
            }
        }
        
        private void Patrol()
        {
            OnEndattack.Invoke();
            _enemyMovement._isPatrol = true;
        }

        private void AttackInit()
        {
            if (_enemyAttack1BlockArea._groggy == null)
                _enemyAttack1BlockArea._groggy = OnGroggy;
            if (_enemyAttack2BlockArea._groggy == null)
                _enemyAttack2BlockArea._groggy = OnGroggy;
            if (_enemyAttack3BlockArea._groggy == null)
                _enemyAttack3BlockArea._groggy = OnGroggy;
            _enemyMovement.OnWalk.Invoke(0f);
            _enemyMovement._isPatrol = false;
            _enemyMovement._isAttack = true;
        }

        public void EndAttack()
        {
            _state = State.None;
        }

        public void IsAttackEnd()
        {
            _enemyMovement._isAttack = false;
        }
        private void SetPattern(int idx)
        {
            _pattern = _patternlist[idx];
            _pattern.Initialise(this);
        }

        private void StartPattern()
        {
            _currentPattern = StartCoroutine(_pattern.Pattern());
        }

        public void StopPattern()
        {
            StopCoroutine(_currentPattern);
            OnEndattack.Invoke();
            OnEndDownAttack.Invoke();
            
            _enemyAttack1BlockArea.DeActiveBlockArea();
            _enemyAttack2BlockArea.DeActiveBlockArea();
            _enemyAttack3BlockArea.DeActiveBlockArea();
            
            _enemyAttack1BlockArea._isHit = true;
            _enemyAttack2BlockArea._isHit = true;
            _enemyAttack3BlockArea._isHit = true;
        }

        public void Hit()
        {
            _enemyAttack1BlockArea._isHit = false;
            _enemyAttack2BlockArea._isHit = false;
            _enemyAttack3BlockArea._isHit = false;
            _state = State.None;
            _enemyMovement._isHit = false;
            OnEndHit.Invoke();
        }
        
        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }

    }
}