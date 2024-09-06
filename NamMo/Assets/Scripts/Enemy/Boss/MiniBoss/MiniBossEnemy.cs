using System;
using System.Collections;
using Enemy.Boss.MiniBoss.State;
using Enemy.WaveAttack;
using NamMo;
using UnityEngine;
using Random = System.Random;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossEnemy: Enemy, IWaveAttacker
    {
        public MiniBossStateMachine _miniBossStateMachine;
        [SerializeField] private MiniBossDashAttackPattern _miniBossDashAttackPattern;
        [SerializeField] private MiniBossMeleeAttackPattern _miniBossMeleeAttackPattern;
        [SerializeField] private MiniBossLandAttackPattern _miniBossLandAttackPattern;
        
        [SerializeField] public EnemyAttackArea EnemyMelAttack1AttackArea;
        [SerializeField] public EnemyAttackArea EnemyMelAttack2AttackArea;
        [SerializeField] public EnemyAttackArea EnemyMelAttack3AttackArea;
        [SerializeField] public EnemyAttackArea EnemyDashAttackAttackArea;

        public GameObject _enemyWavePrefab;
        
        public float melAttack1Time;
        public float melAttack2Time;
        public float melAttack3Time;
        public float dashAttackTime;
        public float landAttackTime;
        
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
            
            EnemyMelAttack1AttackArea.SetAttackInfo(gameObject, 2);
            EnemyMelAttack2AttackArea.SetAttackInfo(gameObject, 2);
            EnemyMelAttack3AttackArea.SetAttackInfo(gameObject, 2);
            EnemyDashAttackAttackArea.SetAttackInfo(gameObject, 2);
        }

        public override void Behavire(float distance)
        {
            _miniBossStateMachine.Update();
            _distance = distance;
        }

        public void GroggyEnter()
        {
            EnemyMelAttack1AttackArea._groggy += OnGroggy;
            EnemyMelAttack2AttackArea._groggy += OnGroggy;
            EnemyMelAttack3AttackArea._groggy += OnGroggy;
            EnemyDashAttackAttackArea._groggy += OnGroggy;
        }
        

        public void TransitionToIdel()
        {
            _miniBossStateMachine.TransitionState(_miniBossStateMachine.IdelState);
        }

        public void TransitionToGroggy()
        {
            _miniBossStateMachine.TransitionState(_miniBossStateMachine._GroggyState);
        }

        public void Groggy()
        {
            _enemyMovement._isGroggy = true;
        }

        public void EndGroggy()
        {
            _enemyMovement._isGroggy = false;
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
            OnDashAttack.Invoke();
        }

        public void EndDashAttack()
        {
            OnEndDashAttack.Invoke();
        }

        public void LandAttack()
        {
            OnLandAttack.Invoke();
        }

        public void EndLandAttack()
        {
            OnEndLandAttack.Invoke();
        }

        public void DeActivateAttackArea()
        {
            EnemyDashAttackAttackArea.DeActiveAttackArea();
            EnemyMelAttack1AttackArea.DeActiveAttackArea();
            EnemyMelAttack2AttackArea.DeActiveAttackArea();
            EnemyMelAttack3AttackArea.DeActiveAttackArea();
        }
        
        public void MelAttackPatternStart()
        {
            Debug.Log("MelAttack Start");
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            StartCoroutine(_miniBossMeleeAttackPattern.Pattern());
        }

        public void DashAttackPatternStart()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            StartCoroutine(_miniBossDashAttackPattern.Pattern());
        }

        public void LandAttackPatternStart()
        {
            StartCoroutine(_miniBossLandAttackPattern.Pattern());
        }

        public void ShootWave()
        {
            GameObject go = Instantiate(_enemyWavePrefab, transform.position, Quaternion.identity);
            EnemyWave wave = go.GetComponent<EnemyWave>();
            wave.DoWave(this);
        }
        
        public void WaveParried()
        {
            OnGroggy.Invoke();
        }

        public Transform GetPosition()
        {
            return gameObject.transform;
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