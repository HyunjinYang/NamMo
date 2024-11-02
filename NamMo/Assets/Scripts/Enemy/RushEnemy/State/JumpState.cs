using System;
using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.RushEnemy.State
{
    public class JumpState: IStateClass
    {
        public RushEnemy _RushEnemy;

        private Vector2 target;
        private float speed;
        public JumpState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
        }
        
        public void Enter()
        {
            _RushEnemy.OnJump.Invoke();

            target = Managers.Scene.CurrentScene.Player.transform.position;

            if (target.x <= _RushEnemy.gameObject.transform.position.x)
            {
                target.x -= 3.5f;
            }
            else
            {
                target.x += 3.5f;
            }

            speed = Math.Abs(Vector2.Distance(_RushEnemy.transform.position, target)) / 1f;
        }

        public void Update()
        {
            Vector2 _curr = _RushEnemy.gameObject.transform.position;

            if (Math.Abs(_curr.x - target.x) < 1f)
            {
                _RushEnemy._statemachine.TransitionState(_RushEnemy._statemachine._RushAttackState);
                return;
            }
            
            _curr.x = Mathf.MoveTowards(_curr.x, target.x, 14 * Time.deltaTime);
            _RushEnemy.transform.position = _curr;
        }

        public void Exit()
        {
            _RushEnemy.EndJumpAnim();
        }
    }
}