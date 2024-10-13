using System;
using Enemy.MelEnemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class DashAttackState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        private Vector2 target;

        private float _speed;
        public DashAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.DashAttackPatternStart();
            
            target = Managers.Scene.CurrentScene.Player.transform.position;

            if (target.x <= _MiniBossEnemy.gameObject.transform.position.x)
            {
                target.x += 4.5f;
            }
            else
            {
                target.x -= 4.5f;
            }

            _speed = Math.Abs(Vector2.Distance(_MiniBossEnemy.transform.position, target)) / 1.4f;

        }

        public void Update()
        {
            if (!_MiniBossEnemy._isAttacking)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.TurmState);
            }
            
            Vector2 _curr = _MiniBossEnemy.gameObject.transform.position;
            _curr.x = Mathf.MoveTowards(_curr.x, target.x, _speed * Time.deltaTime);
            _MiniBossEnemy.transform.position = _curr;
            
        }

        public void Exit()
        {
            
        }
    }
}