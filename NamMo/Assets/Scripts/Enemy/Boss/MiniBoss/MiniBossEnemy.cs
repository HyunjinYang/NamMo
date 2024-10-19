using System;
using System.Collections;
using Contents.Scene;
using Enemy.Boss.MiniBoss.State;
using Enemy.WaveAttack;
using NamMo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossEnemy: Enemy, IWaveAttacker
    {
        public MiniBossStateMachine _miniBossStateMachine;
        [SerializeField] private MiniBossDashAttackPattern _miniBossDashAttackPattern;
        [SerializeField] private MiniBossMeleeAttackPattern _miniBossMeleeAttackPattern;
        [FormerlySerializedAs("_miniBossLandAttackPattern")] [SerializeField] private MiniBossWaveAttackPattern miniBossWaveAttackPattern;
        [SerializeField] private MiniBossRecoveryPattern _miniBossRecoveryPattern;
        [SerializeField] private MinBossLandAttackPattern _minBossLandAttackPattern;
        
        [SerializeField] public EnemyAttackArea EnemyMelAttack1AttackArea;
        [SerializeField] public EnemyAttackArea EnemyMelAttack2AttackArea;
        [SerializeField] public EnemyAttackArea EnemyMelAttack3AttackArea;
        [SerializeField] public EnemyAttackArea EnemyDashAttackAttackArea;
        [SerializeField] public EnemyAttackArea EnemyLandAttackArea;
        //[SerializeField] public EnemyAttackArea EnemyGrapAttackArea;

        public GameObject _teleport;
        
        public GameObject _enemyWavePrefab;
        
        public float melAttack1Time;
        public float melAttack2Time;
        public float melAttack3Time;
        public float dashAttackTime;
        public float waveAttackTime;
        public float landAttackTime;
        public float grapAttackTime;
        
        private Animator _animator;
        
        public float _distance;
        
        private Random _rand = new Random();

        public int _isMelAttack;
        public int _attack1count;
        public int _attack2count;

        public bool _isAttacking;

        public int phase = 1;

        private Coroutine _currentPattern;
        private Coroutine _TurmCoroutine;
        private Coroutine _TrackingCoroutine;
        
        public Action OnAttack2;
        public Action OnDashAttack;
        public Action OnWaveAttack;
        public Action OnEndAttack2;
        public Action OnEndDashAttack;
        public Action OnEndWaveAttack;
        public Action OnChangePhase;
        public Action OnEndChangePhase;
        public Action OnHealthRecovery;
        public Action OnEndHealthRecovery;
        public Action OnLandAttack;
        public Action OnEndLandAttack;
        public Action OnGrapAttack;
        public Action OnEndGrapAttack;

        public bool idelTurm = true;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _miniBossStateMachine = new MiniBossStateMachine(this);
            _miniBossStateMachine.Initalizze(_miniBossStateMachine._EntryState);
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            SceneLinkedSMB<MiniBossEnemy>.Initialise(_animator, this);
            
            _miniBossDashAttackPattern.Initialise(this);
            miniBossWaveAttackPattern.Initialise(this);
            _miniBossMeleeAttackPattern.Initialise(this);
            _minBossLandAttackPattern.Initialise(this);
            _miniBossRecoveryPattern.Initialise(this);
            
            EnemyMelAttack1AttackArea.SetAttackInfo(gameObject, 2);
            EnemyMelAttack2AttackArea.SetAttackInfo(gameObject, 2);
            EnemyMelAttack3AttackArea.SetAttackInfo(gameObject, 2, 3);
            EnemyLandAttackArea.SetAttackInfo(gameObject,2);
            EnemyDashAttackAttackArea.SetAttackInfo(gameObject, 2);
            //EnemyGrapAttackArea.SetAttackInfo(gameObject, 2);
        }

        public override void Behavire(float distance)
        {
            if (_miniBossStateMachine != null)
            {
                if (_hp <= maxhp / 2 && (_miniBossStateMachine._CurrentState is IdelState || _miniBossStateMachine._CurrentState is TurmState) && phase == 1)
                {
                    phase = 2;
                    _hp = 5;
                    _miniBossStateMachine.TransitionState(_miniBossStateMachine._ChangePhaseState);
                }
                _distance = distance;
                _miniBossStateMachine.Update();
            }
        }

        public override void GroggyEnter()
        {
            EnemyMelAttack1AttackArea._groggy += OnGroggy;
            EnemyMelAttack2AttackArea._groggy += OnGroggy;
            EnemyMelAttack3AttackArea._groggy += OnGroggy;
            EnemyDashAttackAttackArea._groggy += OnGroggy;
        }

        public bool IsDead()
        {
            if (_hp <= 0)
                return true;
            else
                return false;
        }


        public override void GroggyStetCount()
        {
            if (_miniBossStateMachine._CurrentState is MeleeAttackState)
            {
                currentgroggyStet += 0.3f;
            }
            else if (_miniBossStateMachine._CurrentState is DashAttackState)
            {
                currentgroggyStet += 0.5f;
            }
            else if (_miniBossStateMachine._CurrentState is WaveAttackState)
            {
                currentgroggyStet += 0.5f;
            }
        }


        public void TransitionToIdel()
        {
            _miniBossStateMachine.TransitionState(_miniBossStateMachine.IdelState);
        }

        public void TransitionToGroggy()
        {
            _miniBossStateMachine.TransitionState(_miniBossStateMachine._GroggyState);
        }

        #region Animation
        
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

            if (_attack1count >= 2)
            {
                _isMelAttack = 1;
                next = 1;
                _attack1count = 0;
            }

            if (_attack2count >= 2)
            {
                _isMelAttack = 0;
                next = 0;
                _attack2count = 0;
            }
            
            
            if (next == 0)
            {
                _attack1count++;
                Onattack.Invoke();
            }
            else
            {
                _attack2count++;
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

        public void WaveAttack()
        {
            OnWaveAttack.Invoke();
        }

        public void EndWaveAttack()
        {
            OnEndWaveAttack.Invoke();
        }

        public void ChangePhase()
        {
            OnChangePhase.Invoke();
        }

        public void EndChangePhase()
        {
            OnEndChangePhase.Invoke();
        }

        public void HealthRecovery()
        {
            OnHealthRecovery.Invoke();
        }
        public void EndHealthRecovery()
        {
            OnEndHealthRecovery.Invoke();
        }

        public void Walk()
        {
            OnWalk.Invoke(1f);
        }
        public void EndWalk()
        {
            OnWalk.Invoke(0f);   
        }
        #endregion
        public void DeActivateAttackArea()
        {
            
        }

        #region Pattern
        
        public void MelAttackPatternStart()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            _currentPattern = StartCoroutine(_miniBossMeleeAttackPattern.Pattern());
        }

        public void DashAttackPatternStart()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            _currentPattern = StartCoroutine(_miniBossDashAttackPattern.Pattern());
        }

        public void RecoveryPatternStart()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            _currentPattern = StartCoroutine(_miniBossRecoveryPattern.Pattern());
        }

        public void WaveAttackPatternStart()
        {
            _currentPattern = StartCoroutine(miniBossWaveAttackPattern.Pattern());
        }

        public void LandAttackPatternStart()
        {
            _currentPattern = StartCoroutine(_minBossLandAttackPattern.Pattern());
        }
        
        public void StopPattern()
        {
            EndMelAttack();
            EndDashAttack();
            EndWaveAttack();
            StopCoroutine(_currentPattern);
        }

        public void PatternSelect()
        {
            var next = _rand.Next(0, 2);

            if (next == 1)
            {
                _miniBossStateMachine.TransitionState(_miniBossStateMachine._LandAttackState);
            }
            else
            {
                _miniBossStateMachine.TransitionState(_miniBossStateMachine._WaveAttackState);
            }
        }

        public bool HealSelect()
        {
            var next = _rand.Next(0, 2);

            if (next == 0)
            {
                return true;
            }

            return false;
        }
        
        #endregion

        
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

        public void PositionTransition()
        {
            if (Math.Abs(Vector2.Distance(gameObject.transform.position,
                    Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._leftPoint.position)) <
                Math.Abs(Vector2.Distance(gameObject.transform.position,
                    Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._rightPoint.position)))
            {
                gameObject.transform.position = Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._rightPoint.position;
            }
            else
            {
                gameObject.transform.position = Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._leftPoint.position;
            }
        }
        
        public void StartTurm()
        {
            _TurmCoroutine = StartCoroutine(CoTurm());
        }

        public void StopTurm()
        {
            if(_TurmCoroutine != null)
                StopCoroutine(_TurmCoroutine);
            _TurmCoroutine = null;
        }

        public void StartTracking()
        {
            _TrackingCoroutine = StartCoroutine(CoTracking());
        }

        public void StopTracking()
        {
            StopCoroutine(_TrackingCoroutine);
            _TrackingCoroutine = null;
        }

        public void Direct()
        {
            _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
        }

        public void StartIdelTurm()
        {
            StartCoroutine(CoIdelTurm());
        }

        private IEnumerator CoIdelTurm()
        {
            yield return new WaitForSeconds(0.2f);
            idelTurm = false;
        }
        
        public IEnumerator CoTurm()
        {
            yield return new WaitForSeconds(1.5f);
            TransitionToIdel();
        }

        public IEnumerator CoTracking()
        {
            yield return new WaitForSeconds(2f);
            TransitionToIdel();
        }
    }
}