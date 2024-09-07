using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimController: MonoBehaviour
    {
        [SerializeField] private EnemyMovement _em;
        [SerializeField] protected Enemy _enemy;
        protected Animator _animator;
        private float _moveDir = 0;

        protected virtual void Start()
        {
            _em.OnWalk += Walk;

            _enemy.Onattack += Attack;
            _enemy.OnEndattack += EndAttack;
            _enemy.OnHit += Hit;
            _enemy.OnEndHit += EndHit;
            _enemy.Dead += Dead;
            _enemy.OnGroggy += Groggy;
            _enemy.OnEndGroggy += EndGroggy;
            
            _animator = GetComponent<Animator>();
        }

        private void Walk(float dir)
        {
            if (_moveDir != dir)
            {
                _moveDir = dir;

                if (_moveDir == 0)
                {
                    _animator.SetBool("IsWalk", false);
                }
                else
                {
                    _animator.SetBool("IsWalk", true);
                }
            }
        }

        private void Hit()
        {
            if (_em._isHit)
            {
                _animator.Play("Hit", -1, 0f);
            }
            else
                _animator.SetBool("IsHit", true);
        }

        private void EndHit()
        {
            _animator.SetBool("IsHit", false);
        }
        private void Attack()
        {
            _animator.SetBool("IsAttack", true);
        }

        private void EndAttack()
        {
            _animator.SetBool("IsAttack", false);
        }

        private void Dead()
        {
            _animator.SetBool("IsDead", true);
        }

        private void Groggy()
        {
            _enemy.GroggyStetCount();
            
            if (_enemy.currentgroggyStet >= _enemy.maxGroggyStet)
            {
                _enemy.currentgroggyStet = 0f;
                Debug.Log(_enemy.currentgroggyStet + " " + _enemy.maxGroggyStet);
                _animator.SetBool("IsGroggy", true);
            }
        }

        private void EndGroggy()
        {
            _animator.SetBool("IsGroggy", false);
        }
        
    }
}