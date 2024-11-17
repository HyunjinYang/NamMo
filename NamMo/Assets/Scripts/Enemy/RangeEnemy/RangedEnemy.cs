using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.MelEnemy;
using Enemy.State;
using NamMo;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class RangedEnemy : Enemy
    {
        public Action OnRangeAttack;
        public Action OnEndRangeAttack;
        
        
        [SerializeField] private GameObject archr;
        [SerializeField] public EnemyAttackArea _enemyAttackArea;

        [SerializeField] private EnemyAttackPattern<RangedEnemy> _melAttackPattern;
        [SerializeField] private EnemyAttackPattern<RangedEnemy> _rangeAttackPattern;

        private Coroutine _currentPattern;
        
        private Animator _animator;
        
        public RangeEnemyStateMachine stateMachine;

        public float _AttackTime1;
        public float _AttackTime2;

        public bool _isAttacking = false;

        protected override void Start()
        {
            base.Start();
            
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<RangedEnemy>.Initialise(_animator, this);

            stateMachine = new RangeEnemyStateMachine(this);
            stateMachine.Initialize(stateMachine._IdelState);

            _distance = Int64.MaxValue;
            _enemyAttackArea.SetAttackInfo(gameObject, 2);
            
        }

        private void Update()
        {
            stateMachine.Update();
        }

        public override void GroggyEnter()
        {
            _enemyAttackArea._groggy += Groggy;
        }

        public void Groggy()
        {
            OnGroggy.Invoke();
        }

        public void MelAttackAnim()
        {
            Onattack.Invoke();
        }

        public void EndMelAttackAnim()
        {
            OnEndattack.Invoke();
        }

        public void RangeAttackAnim()
        {
            OnRangeAttack.Invoke();
        }

        public void EndRangeAttackAnim()
        {
            OnEndRangeAttack.Invoke();
        }
        public override void Behavire(float distance)
        {
            base.Behavire(distance);

            _distance = distance;
            stateMachine.Update();
        }
        
        
        public void MelAttack()
        {
            _enemyMovement.OnWalk(0f);
            _melAttackPattern.Initalize(this);
            _currentPattern = StartCoroutine(_melAttackPattern.Pattern());
        }

        public void RangeAttack()
        {
            _enemyMovement.OnWalk(0f);
            _rangeAttackPattern.Initalize(this);
            _currentPattern = StartCoroutine(_rangeAttackPattern.Pattern());
        }

        public void StopPattern()
        {
            EndMelAttackAnim();
            EndRangeAttackAnim();
            StopCoroutine(_currentPattern);
        }
        public void CreateRangeAttack()
        {
            GameObject cur =Instantiate(archr, gameObject.transform.position, transform.rotation);
            cur.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 1f, 1, 4, Managers.Scene.CurrentScene.Player.gameObject);
            cur.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (cur)
                {
                    Managers.Resource.Destroy(cur);
                }
            });
        }
        

        public void MelAttackPatternStart()
        {
            _enemyMovement.DirectCheck();
            MelAttack();
        }

        public void RangeAttackPatternStart()
        {
            _enemyMovement.DirectCheck();
            RangeAttack();
        }

        public void Direct()
        {
            _enemyMovement.DirectCheck();
        }
        

        public void EndWalk()
        {
            _enemyMovement.OnWalk(0f);
        }

        public void TransitionHit()
        {
            stateMachine.TransitionState(stateMachine._HitState);
        }

        public void TransitionEndHit()
        {
            stateMachine.TransitionState(stateMachine._PatrolState);
        }

        public void TransitionGroggy()
        {
            stateMachine.TransitionState(stateMachine._GroggyState);
        }

        public void TranstionIdel()
        {
            stateMachine.TransitionState(stateMachine._IdelState);
        }

        public override void PlayerTrackingState()
        {
            stateMachine.TransitionState(stateMachine._PatrolState);
        }

        public void SetHit(bool isActivate)
        {
            _enemyMovement._isHit = isActivate;
            OnEndHit.Invoke();
        }

        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }


        public override IEnumerator CoTurm()
        {
            
            yield return new WaitForSeconds(2f);
            stateMachine.TransitionState(stateMachine._PatrolState);
        }

    }
}