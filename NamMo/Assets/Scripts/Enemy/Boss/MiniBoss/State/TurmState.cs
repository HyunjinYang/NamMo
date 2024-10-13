using System.Collections;
using Enemy.MelEnemy;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

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
            _MiniBossEnemy.Direct();
            if (_MiniBossEnemy.HP < 7 && _MiniBossEnemy.HealSelect())
            {
                return;
            }
            
            if (Math.Abs(Vector2.Distance(_curr, _target)) > 7.5f && Math.Abs(Vector2.Distance(_curr, _target)) <= 10.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.IdelState);
            }
            else if (Math.Abs(Vector2.Distance(_curr, _target)) > 10.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine._PatrolState);
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