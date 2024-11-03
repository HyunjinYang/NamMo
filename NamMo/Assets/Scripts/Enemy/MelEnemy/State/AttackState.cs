using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.MelEnemy
{
    public class AttackState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public AttackState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("AttackState Start");
            _MelEnemy.GetComponent<NavMeshAgent>().isStopped = true;
            _MelEnemy.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            _MelEnemy.Attack();
            _MelEnemy.GroggyEnter();
        }

        public void Update()
        {
            if (_MelEnemy.isTest)
            {
                if (_MelEnemy._distance > 6.7f && !_MelEnemy.IsAttacking)
                {
                    _MelEnemy.stateMachine.TransitionState(_MelEnemy.stateMachine._patrolstate);
                }
            }
            else
            {
                if (_MelEnemy._distance > 3.9f && !_MelEnemy.IsAttacking)
                {
                    _MelEnemy.stateMachine.TransitionState(_MelEnemy.stateMachine._patrolstate);
                }
            }
        }

        public void Exit()
        {
            _MelEnemy.EndAttack();
            _MelEnemy.GroggyExit();
        }
    }
}
