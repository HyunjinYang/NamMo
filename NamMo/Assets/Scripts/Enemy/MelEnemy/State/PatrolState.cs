using UnityEngine;

namespace Enemy.MelEnemy
{
    public class PatrolState: IStateClass
    {
        public MelEnemy _melEnemy;

        public PatrolState(MelEnemy _melEnemy)
        {
            this._melEnemy = _melEnemy;
        }
        public void Enter()
        {
            Debug.Log("PatrolState");
           _melEnemy.Patrol();
        }

        public void Update()
        {
            if (_melEnemy.ReturnIsDead())
            {
                _melEnemy.stateMachine.TransitionState(_melEnemy.stateMachine._DeadState);
            }

            if (_melEnemy.isTest)
            {
                if (_melEnemy._distance <= 5.7f)
                {
                    _melEnemy.stateMachine.TransitionState(_melEnemy.stateMachine._AttackState);
                }
            }
            else
            {
                if (_melEnemy._distance <= 3f)
                {
                    _melEnemy.stateMachine.TransitionState(_melEnemy.stateMachine._AttackState);
                }
            }
        }

        public void Exit()
        {
            _melEnemy.EndPatrol();
        }
    }
}