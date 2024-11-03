using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.RushEnemy.State
{
    public class PatrolState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public PatrolState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("PatrolState");
            
            _RushEnemy.GetComponent<NavMeshAgent>().isStopped = false;
        }

        public void Update()
        {
            if (_RushEnemy.ReturnIsDead())
            {
                _RushEnemy._statemachine.TransitionState(_RushEnemy._statemachine._DeadState);
                return;
            }
            
            _RushEnemy.Tracking();

            if (_RushEnemy._distance <= 9f)
            {
                _RushEnemy._statemachine.TransitionState(_RushEnemy._statemachine._JumpState);
                return;
            }
        }

        public void Exit()
        {
            
        }
    }
}