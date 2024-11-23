using System;
using System.Collections;
using Enemy.Boss.MiniBoss;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int _hp;
        [SerializeField] protected int maxhp;
        public float _distance;
        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                if (_hp + value >= 10)
                    _hp = 10;
                else
                    _hp += value;
            }
        }
        
        protected GameObject _player;
        [SerializeField] protected EnemyMovement _enemyMovement;
        public Action Onattack;
        public Action OnEndattack;
        public Action OnHit;
        public Action OnEndHit;
        public Action DeadEvent;
        public Action OnGroggy;
        public Action OnEndGroggy;
        public Action<float> OnWalk;
        public int CurrentAttackCount;
        
        protected Coroutine _TurmCoroutine;

        [SerializeField] private GameObject _waveDetectEffectPrefab;
        private GameObject _waveEffect;
        [SerializeField]private float _scaleChangeTime;
        [SerializeField] private float _size;
        [SerializeField] private GameObject _waveGroundEffect;
        public void ShowWaveVFX()
        {
            _waveEffect = Instantiate(_waveDetectEffectPrefab, gameObject.transform.position, Quaternion.identity);
            _waveEffect.GetComponent<VFXController>().Play(_scaleChangeTime, _size);
        }
        
        public int ManagedId { get; set; } = -1;

        private bool _isFacing = true;
        public bool IsFacingRight
        {
            get
            {
                return _isFacing;
            }
            set
            {
                _isFacing = value;
            }
        }
        

        public float maxGroggyStet;
        public float currentgroggyStet;
        
        public bool _isParingAvailable = false;
        public bool _isinvincibility;

        private EnemyDamageFlash _enemyDamageFlash;

        protected virtual void Start()
        {
            _enemyDamageFlash = GetComponent<EnemyDamageFlash>();
        }

        public virtual void Behavire(float distance)
        {
            if (distance <= 5f)
            {
                _waveGroundEffect.SetActive(false);
            }
            else
            {
                _waveGroundEffect.SetActive(true);
            }
        }

        public void PlayerFind(GameObject player)
        {
            _player = player;
            _enemyMovement._playerposition = _player.transform;
        }

        public void PlayerExit()
        {
            _player = null;
            _enemyMovement = null;
        }

        public void Hit(int damage)
        {
            if (_isinvincibility)
                return;
            
            _hp -= damage;
            _enemyDamageFlash.CallDamageFlash();
            
            if (_hp <= 0)
            {
                OnDead();
                return;
            }
            
            if(gameObject.GetComponent<MiniBossEnemy>() == null)
                OnHit.Invoke();
            _enemyMovement._isHit = true;
        }

        public void OnDead()
        {
            _enemyMovement._isDead = true;
            DeadEvent.Invoke();
            gameObject.GetComponent<NavMeshAgent>().ResetPath();
            if (ManagedId != -1)
            {
                Managers.Data.EnemyData.KillEnemy(Managers.Scene.CurrentScene.SceneType, ManagedId);
            }
        }

        public bool ReturnIsHit()
        {
            return _enemyMovement._isHit;
        }

        public bool ReturnIsDead()
        {
            return _enemyMovement._isDead;
        }
        public void EndHit()
        {
            _enemyMovement._isHit = false;
            OnEndHit.Invoke();
        }

        public int GetHp()
        {
            return _hp;
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

        public virtual IEnumerator CoTurm()
        {
            return null;
        }

        public void Patrol()
        {
            _enemyMovement.Patrol();
        }

        public void Tracking()
        {
            _enemyMovement.PlayerTracking();
        }
        
        public virtual void GroggyStetCount()
        {
            
        }

        public virtual void GroggyEnter()
        {
            
        }

        public virtual void PlayerTrackingState()
        {
            
        }
        
        
        
    }
}