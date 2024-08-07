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
    }
}