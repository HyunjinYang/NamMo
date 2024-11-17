using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.State
{
    public class RangeAttackState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public RangeAttackState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy RangeAttack State");   
            _RangedEnemy._isAttacking = true;
            
            _RangedEnemy.GetComponent<NavMeshAgent>().isStopped = true;
            _RangedEnemy.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            _RangedEnemy.RangeAttackPatternStart();
            _RangedEnemy.Direct();
        }

        public void Update()
        {
            if (!_RangedEnemy._isAttacking)
            {
                //_RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._RangeAttackState);
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._TurmState);
            }
        }
        public void Exit()
        {
            
        }
    }
}