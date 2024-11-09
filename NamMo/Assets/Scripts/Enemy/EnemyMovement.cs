using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyMovement: MonoBehaviour
    {
        [SerializeField] protected GameObject _CharacterSprite;
        [SerializeField] protected Transform _point1;
        [SerializeField] protected Transform _point2;
        [SerializeField] private bool _isNextPoint = false;
        [SerializeField] private bool _isWait = false;
        [SerializeField] public bool _isAttack = false;
        public bool _isHit = false;
        public bool _isDead = false;
        public bool _isPatrol = false;
        public bool _isGroggy = false;
        private Transform _currentWayPoint;
        private Rigidbody2D _rb;
        private float _distance = 0.9f;
        public Action OnAttack;
        public Action<float> OnWalk;
        public Transform _playerposition;
        [SerializeField] private int direct = 1;
        private NavMeshAgent _agent;
        public float _speed;

        private float timer;
        private float updateInterval = 0.6f;
        protected  void Awake()
        {
            Flip();
            _rb = GetComponent<Rigidbody2D>();
            _currentWayPoint = _point1;
            _agent = _CharacterSprite.GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = _speed;
            _agent.SetDestination(_currentWayPoint.position);
        }
        public void Patrol()
        {
            if (!_isWait)
            {
                OnWalk.Invoke(1f);
                _agent.isStopped = false;
                if (Vector2.Distance(_CharacterSprite.transform.position, _currentWayPoint.position) <= _distance)
                {
                    StartCoroutine(NextMove());
                }
            }
        }

        public void PlayerTracking()
        {
            /*if ((_CharacterSprite.transform.position.x < _point1.position.x ||
                 _CharacterSprite.transform.position.x > _point2.position.x) 
                && (_playerposition.position.x < _point1.position.x || _playerposition.position.x > _point2.position.x))
            {
                OnWalk.Invoke(0f);
                return;
            }*/

            OnWalk.Invoke(1f);
            if (_playerposition == null)
                _playerposition = Managers.Scene.CurrentScene.Player.transform;
            
            /*
            Vector2 _curr = _CharacterSprite.transform.position;
            _curr.x = Mathf.MoveTowards(_curr.x, _playerposition.position.x, _speed * Time.deltaTime);
            _CharacterSprite.transform.position = _curr;*/
            timer += Time.deltaTime;
            
            if(timer >= updateInterval)
                _agent.SetDestination(_playerposition.position);
            DirectCheck();

        }
        private IEnumerator NextMove()
        {
            OnWalk.Invoke(0f);
            _agent.ResetPath();
            _isWait = true;
            yield return new WaitForSeconds(2f);
            if (!_isNextPoint)
            {
                _currentWayPoint = _point2;
            }
            else
            {
                _currentWayPoint = _point1;
            }
            _agent.SetDestination(_currentWayPoint.position);
            Flip();
            _isWait = false;
            _isNextPoint = !_isNextPoint;
            
        }

        public void DirectCheck()
        {
            Debug.Log(_agent.velocity.x);
            if (_agent.velocity.x == 0)
                return;
            
            if (0 < _agent.velocity.x)
            {
                if (direct == 1)
                    return;
                else
                {
                    Flip();
                }
            }
            else
            {
                if (direct == -1)
                    return;
                else
                {
                    Flip();
                }
            }
            
        }
        
        public void Direct(float x, float targetx)
        {
            Debug.Log(direct);
            if (0 < x)
            {
                if (direct == -1)
                    return;
                else
                {
                    Flip();
                }
            }
            else
            {
                if (direct == 1)
                    return;
                else
                {
                    Flip();
                }
            }
            
        }
        private void Facing()
        {
            if (direct == 1)
            {
                
            }
            else
            {
                
            }
        }
        protected  void Flip()
        {
            Vector3 localScale = _CharacterSprite.transform.localScale;
            localScale.x *= -1f;
            if (localScale.x < 0)
            {
                _CharacterSprite.GetComponent<Enemy>().IsFacingRight = true;
            }
            else
            {
                _CharacterSprite.GetComponent<Enemy>().IsFacingRight = false;
            }
            direct *= -1;
            _CharacterSprite.transform.localScale = localScale;
        }

    }
}