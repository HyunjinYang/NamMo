using System;
using Enemy;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class TestEnemy : MonoBehaviour
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
        public Action Onattack;
        public Action OnEndattack;
        public Action OnHit;
        public Action OnEndHit;
        public Action DeadEvent;
        public Action OnGroggy;
        public Action OnEndGroggy;
        public Action<float> OnWalk;
        public int CurrentAttackCount;
        
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

        private float groggyCount;
        
        public int ManagedId { get; set; } = -1;
        
        

        public float maxGroggyStet;
        public float currentgroggyStet;
        
        public bool _isinvincibility;

        [SerializeField] private EnemyDamageFlash _enemyDamageFlash;

        protected virtual void Start()
        {
            OnGroggy = Groggy;
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