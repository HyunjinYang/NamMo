using System;
using System.Collections;
using Enemy.Boss.MiniBoss.State;
using NamMo;
using UnityEngine;
using Random = System.Random;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossEnemy: Enemy
    {
        public MiniBossStateMachine _miniBossStateMachine;
        [SerializeField] private MiniBossDashAttackPattern _miniBossDashAttackPattern;
        [SerializeField] private MiniBossMeleeAttackPattern _miniBossMeleeAttackPattern;
        [SerializeField] private MiniBossLandAttackPattern _miniBossLandAttackPattern;
        
        [SerializeField] public EnemyBlockArea _enemyMelAttack1BlockArea;
        [SerializeField] public EnemyBlockArea _enemyMelAttack2BlockArea;
        [SerializeField] public EnemyBlockArea _enemyMelAttack3BlockArea;
        [SerializeField] public EnemyBlockArea _enemyDashAttackBlockArea;

        public float melAttack1Time;
        public float melAttack2Time;
        public float melAttack3Time;
        public float dashAttackTime;
        
        private Animator _animator;
        
        public float _distance;
        
        private Random _rand = new Random();

        public int _isMelAttack;

        public bool _isAttacking;
        
        public Action OnAttack2;
        public Action OnDashAttack;
        public Action OnLandAttack;
        public Action OnEndAttack2;
        public Action OnEndDashAttack;
        public Action OnEndLandAttack;
        
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _miniBossStateMachine = new MiniBossStateMachine(this);
            _miniBossStateMachine.Initalizze(_miniBossStateMachine._EntryState);
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            SceneLinkedSMB<MiniBossEnemy>.Initialise(_animator, this);
            
            _miniBossDashAttackPattern.Initialise(this);
            _miniBossLandAttackPattern.Initialise(this);
            _miniBossMeleeAttackPattern.Initialise(this);
        }

        public override void Behavire(float distance)
        {
            _miniBossStateMachine.Update();
            _distance = distance;
        }
        

        public void TransitionToIdel()
        {
            _miniBossStateMachine.TransitionState(_miniBossStateMachine.IdelState);
        }

        public void MelAttack()
        {
            var next = _rand.Next(0, 2);

            _isMelAttack = next;
            if (next == 0)
            {
                Onattack.Invoke();
            }
            else
            {
                OnAttack2.Invoke();
            }
        }

        public void EndMelAttack()
        {
            if(_isMelAttack == 0)
                OnEndattack.Invoke();
            else
                OnEndAttack2.Invoke();
        }
        
        public void DashAttack()
        {

        }

        public void MelAttackPatternStart()
        {
            Debug.Log("MelAttack Start");
            StartCoroutine(_miniBossMeleeAttackPattern.Pattern());
        }

        public void StartTurm()
        {
            StartCoroutine(CoTurm());
        }

        public IEnumerator CoTurm()
        {
            yield return new WaitForSeconds(2f);
            TransitionToIdel();
        }
    }
}