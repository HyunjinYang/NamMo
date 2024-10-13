using System;
using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class TurmState : IStateClass
    {
        public RangedEnemy _RangedEnemy;
        private Vector2 _target;


        public TurmState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Turm State");   
            if (_RangedEnemy.ReturnIsDead())
            {
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._DeadState);
                return;
            }
            _RangedEnemy.EndWalk();
            _RangedEnemy.StartTurm();
        }

        public void Update()
        {
            Vector2 _curr = _RangedEnemy.transform.position;
            _target = Managers.Scene.CurrentScene.Player.transform.position;

            if (Math.Abs(Vector2.Distance(_curr, _target)) < 5f)
            {
                // 도망치는 코드
            }
        }

        public void Exit()
        {
            _RangedEnemy.StopTurm();
        }
    }
}