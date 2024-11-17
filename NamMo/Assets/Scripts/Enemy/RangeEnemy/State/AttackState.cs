using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.State
{
    public class AttackState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public AttackState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy MelAttack State");   
            Debug.Log(_RangedEnemy.gameObject.name);
            _RangedEnemy._isAttacking = true;
            _RangedEnemy.MelAttackPatternStart();
            _RangedEnemy.GroggyEnter();
            _RangedEnemy.GetComponent<NavMeshAgent>().isStopped = true;
            _RangedEnemy.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        }

        public void Update()
        {
            if (!_RangedEnemy._isAttacking)
            {
                //_RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._TurmState);
            }
        }
        public void Exit()
        {
            
        }
    }
}