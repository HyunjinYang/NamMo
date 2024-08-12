using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : PlayerMovement
    {
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private bool _isNextPoint = false;
        [SerializeField] private bool _isWait = false;
        [SerializeField] public bool _isAttack = false;
        public bool _isHit = false;
        public bool _isDead = false;
        public bool _isPatrol = false;
        private Transform _currentWayPoint;
        private float _distance = 0.1f;
        public Action OnAttack;
        public Transform _playerposition;
        [SerializeField] private int direct = 1;
        protected override void Awake()
        {
            Flip();
            _rb = GetComponent<Rigidbody2D>();
            _currentWayPoint = _point1;
        }

        protected override void FixedUpdate()
        {
            if (_isHit || _isDead)
                return;
            
            if (!_isAttack && !_isWait && !_isPatrol)
            {
                OnWalk.Invoke(1f);
                _CharacterSprite.transform.position = Vector2.MoveTowards(_CharacterSprite.transform.position, _currentWayPoint.position,
                    _speed * Time.deltaTime);

                if (Vector2.Distance(_CharacterSprite.transform.position, _currentWayPoint.position) <= _distance)
                {
                    StartCoroutine(NextMove());
                }
            }

            if (_isPatrol && !_isAttack)
            {
                OnWalk.Invoke(1f);
                DirectCheck(_CharacterSprite.transform.position.x, _playerposition.position.x);
                Vector2 _curr = _CharacterSprite.transform.position;
                _curr.x = Mathf.MoveTowards(_curr.x, _playerposition.position.x, _speed * Time.deltaTime);
                _CharacterSprite.transform.position = _curr;
            }
        }

        private IEnumerator NextMove()
        {
            OnWalk.Invoke(0f);
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
            Flip();
            _isWait = false;
            _isNextPoint = !_isNextPoint;
            
        }

        public void DirectCheck(float x, float targetx)
        {
            if (x == targetx)
                return;
            
            if (x < targetx)
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

        private void Facing()
        {
            if (direct == 1)
            {
                
            }
            else
            {
                
            }
        }
        protected override void Flip()
        {
            Vector3 localScale = _CharacterSprite.transform.localScale;
            localScale.x *= -1f;
            direct *= -1;
            _CharacterSprite.transform.localScale = localScale;
        }

    }
}