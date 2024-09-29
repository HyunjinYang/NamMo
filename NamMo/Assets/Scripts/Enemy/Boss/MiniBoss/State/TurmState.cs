using System.Collections;
using Enemy.MelEnemy;
using System;
using UnityEngine;
namespace Enemy.Boss.MiniBoss.State
{
    public class TurmState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        private Vector2 _target;
        public TurmState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            if (_MiniBossEnemy.IsDead())
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine._DeadState);
                return;
            }
            
            _MiniBossEnemy.StartTurm();
            _MiniBossEnemy.Direct();
            _MiniBossEnemy.Walk();
        }

        public void Update()
        {
            
            
            Vector2 _curr = _MiniBossEnemy.transform.position;
            _target = Managers.Scene.CurrentScene.Player.transform.position;

            
            
            if (Math.Abs(Vector2.Distance(_curr, _target)) > 4.5f)
            {
                _MiniBossEnemy.Walk();
                _MiniBossEnemy.Direct();
                _curr.x = Mathf.MoveTowards(_curr.x, _target.x, 3.5f * Time.deltaTime);
                
                _MiniBossEnemy.transform.position = _curr;
            }
            else
            {
                _MiniBossEnemy.EndWalk();
            }
        }

        public void Exit()
        {
            _MiniBossEnemy.EndWalk();
            _MiniBossEnemy.StopTurm();
        }
    }
}