using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.RushEnemy.State
{
    public class IdelState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public IdelState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("Idel State");
        }

        public void Update()
        {
            if (_RushEnemy._distance <= 10)
            {
                _RushEnemy._statemachine.TransitionState(_RushEnemy._statemachine._PatrolState);
                return;
            }
            _RushEnemy.Patrol();
        }

        public void Exit()
        {
            
        }
    }
}