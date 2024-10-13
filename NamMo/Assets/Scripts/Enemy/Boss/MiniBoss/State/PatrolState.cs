using System;
using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class PatrolState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;
        
        private Vector2 _target;
        public PatrolState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("PatrolState");   
            _MiniBossEnemy.Walk();
            _MiniBossEnemy.StartTracking();
            _MiniBossEnemy.Direct();

        }

        public void Update()
        {
            Vector2 _curr = _MiniBossEnemy.transform.position;
            _target = Managers.Scene.CurrentScene.Player.transform.position;

            if (Math.Abs(Vector2.Distance(_curr, _target)) < 4.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.TurmState);
            }
            
            _MiniBossEnemy.Walk();
            _MiniBossEnemy.Direct();
            _curr.x = Mathf.MoveTowards(_curr.x, _target.x, 2.0f * Time.deltaTime);
                
            _MiniBossEnemy.transform.position = _curr;
        }

        public void Exit()
        {
            _MiniBossEnemy.StopTracking();
            _MiniBossEnemy.EndWalk();
        }
    }
}