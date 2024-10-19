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

        [SerializeField] private RangeEnemyAttackPattern<RangedEnemy> _melAttackPattern;
        [SerializeField] private RangeEnemyAttackPattern<RangedEnemy> _rangeAttackPattern;

        private Coroutine _currentPattern;
        private Coroutine _TurmCoroutine;
        
        private Animator _animator;
        
        public RangeEnemyStateMachine stateMachine;

        public float _distance;
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
            
            _enemyAttackArea.SetAttackInfo(gameObject, 2);
            
        }
        
        public void GroggyEnter()
        {
            _enemyAttackArea._groggy += Groggy;
        }

        public void Groggy()
        {
            OnGroggy.Invoke();
        }

        public void MelAttackAnim()
        {
            Debug.Log(gameObject.name);
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
            _distance = distance;
            stateMachine.Update();
        }
        
        
        public void MelAttack()
        {
            _enemyMovement.OnWalk(0f);
            _melAttackPattern.Initialise(this);
            _currentPattern = StartCoroutine(_melAttackPattern.Pattern());
        }

        public void RangeAttack()
        {
            _enemyMovement.OnWalk(0f);
            _rangeAttackPattern.Initialise(this);
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
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            MelAttack();
        }

        public void RangeAttackPatternStart()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x,Managers.Scene.CurrentScene.Player.transform.position.x);
            RangeAttack();
        }

        public void Patrol()
        {
            _enemyMovement.Patrol();
        }

        public void Tracking()
        {
            _enemyMovement.PlayerTracking();
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
        
        public void SetHit(bool isActivate)
        {
            _enemyMovement._isHit = isActivate;
            OnEndHit.Invoke();
        }

        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }

        public void StartTurm()
        {
            _TurmCoroutine = StartCoroutine(CoTurm());
        }

        public void StopTurm()
        {
            if(_TurmCoroutine != null)
                StopCoroutine(_TurmCoroutine);
        }

        private IEnumerator CoTurm()
        {
            
            yield return new WaitForSeconds(3f);
            stateMachine.TransitionState(stateMachine._IdelState);
        }

    }
}