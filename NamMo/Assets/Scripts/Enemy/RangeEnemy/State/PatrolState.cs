using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.State
{
    public class PatrolState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public PatrolState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Patrol State"); 
            _RangedEnemy.GetComponent<NavMeshAgent>().isStopped = false;


        }

        public void Update()
        {
            if (_RangedEnemy.ReturnIsDead())
            {
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._DeadState);
                return;
            }
            
            _RangedEnemy.Tracking();
            
            if(_RangedEnemy._distance <= 8.5f)
            {
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._RangeAttackState);
            }
        }

        public void Exit()
        {
            
        }
    }
}