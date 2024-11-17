using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.RushEnemy.State
{
    public class RushAttackState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public RushAttackState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RushAttack State");
            _RushEnemy.GetComponent<NavMeshAgent>().isStopped = true;
            _RushEnemy.GetComponent<NavMeshAgent>().velocity = Vector3.zero;

            _RushEnemy.RushAttack();
            _RushEnemy.GroggyEnter();
        }

        public void Update()
        {
            if (!_RushEnemy._isAttacking)
            {
                Debug.Log("턴 스테이트로 이동");
                _RushEnemy._statemachine.TransitionState(_RushEnemy._statemachine._TurmState);
            }
        }

        public void Exit()
        {
            
        }
    }
}