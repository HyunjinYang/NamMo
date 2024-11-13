using UnityEngine;

namespace Enemy.MelEnemy
{
    public class IdelState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public IdelState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("Idel State");
        }

        public void Update()
        {
            if (_MelEnemy._distance <= 8f)
            {
                _MelEnemy.stateMachine.TransitionState(_MelEnemy.stateMachine._patrolstate);
                return;
            }
            _MelEnemy.Patrol();
        }

        public void Exit()
        {
                       
        }
    }
}