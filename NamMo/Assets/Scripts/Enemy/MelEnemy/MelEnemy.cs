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
        
        [SerializeField] private EnemyBlockArea _enemyBlockArea;
        [SerializeField] private List<MelEnemyAttackPattern<Enemy>> _patternlist = new List<MelEnemyAttackPattern<Enemy>>();
        private Animator _animator;
        private MelEnemyAttackPattern<Enemy> _pattern;
        private Random _rand;

        public float Attack1Time1;
        public float Attack1Time2;
        public float Attack2Time;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<MelEnemy>.Initialise(_animator, this);
            
        }

        public override void Behavire(float distance)
        {
            if (distance >= 6.5f)
            {
                Patrol();   
            }
            else if (distance <= 3.0f)
            {
                AttackInit();
                
                var next = _rand.Next(0, 2);
                SetPattern(next);
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
            _enemyMovement.OnWalk.Invoke(0f);
            _enemyMovement._isPatrol = false;
            _enemyMovement._isAttack = true;
        }

        private void EndAttack()
        {
            _enemyBlockArea.DeActiveBlockArea();
            _enemyMovement._isAttack = false;
        }
        private void SetPattern(int idx)
        {
            _pattern = _patternlist[idx];
            _pattern.Initialise(this);
        }

        private void StartPattern()
        {
            StartCoroutine(_pattern.Pattern());
        }

        private void Attack()
        {
            
        }

        private void MelAttack()
        {
            
        }

        private void DownAttack()
        {
            
        }

    }
}