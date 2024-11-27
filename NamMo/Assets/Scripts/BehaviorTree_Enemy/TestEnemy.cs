using System;
using Enemy;
using Enemy.MelEnemy;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class TestEnemy : MonoBehaviour
    {
        [SerializeField] protected int _hp;
        [SerializeField] protected int maxhp;
        public float _distance;

        public Define.EnemyState _EnemyState;
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
        
        public Action DeadEvent;
        public Action OnGroggy;
        public int CurrentAttackCount;
        
        [SerializeField] private GameObject _waveDetectEffectPrefab;
        private GameObject _waveEffect;
        [SerializeField]private float _scaleChangeTime;
        [SerializeField] private float _size;
        [SerializeField] private GameObject _waveGroundEffect;

        [SerializeField] private bool _isFacing = true;
        [SerializeField] public bool _isPlayerCheck = false;

        public EnemySound _EnemySound;
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
        
        public void ShowWaveVFX()
        {
            _waveEffect = Instantiate(_waveDetectEffectPrefab, gameObject.transform.position, Quaternion.identity);
            _waveEffect.GetComponent<VFXController>().Play(_scaleChangeTime, _size);
        }

        private float groggyCount;
        
        public int ManagedId { get; set; } = -1;
        
        

        public float maxGroggyStet;
        public float currentgroggyStet;
        
        public bool _isinvincibility;

        [SerializeField] private EnemyDamageFlash _enemyDamageFlash;

        protected virtual void Start()
        {
            OnGroggy = Groggy;
            _EnemySound = GetComponent<EnemySound>();
        }
        

        public void Hit(int damage)
        {
            if (_isinvincibility)
                return;
            
            _hp -= damage;
            _enemyDamageFlash.CallDamageFlash();
        }

        public void OnDead()
        {
            DeadEvent.Invoke();
            if (ManagedId != -1)
            {
                Managers.Data.EnemyData.KillEnemy(Managers.Scene.CurrentScene.SceneType, ManagedId);
            }
        }
        
        private void Groggy()
        {
            currentgroggyStet += groggyCount;
            CurrentAttackCount--;
        }


        public int GetHp()
        {
            return _hp;
        }

        public void SetGroggyCount(float Count)
        {
            groggyCount = Count;
        }

        public void SetAttackCount(int Count)
        {
            CurrentAttackCount = Count;
        }
    }
}