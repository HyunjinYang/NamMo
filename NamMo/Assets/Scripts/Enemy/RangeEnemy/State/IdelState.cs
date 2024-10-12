using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class IdelState: IStateClass
    {
        public RangedEnemy _RangedEnemy;
        
        public IdelState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Idel State");   
        }

        public void Update()
        {
            if (_RangedEnemy._distance <= 10f)
            {
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._PatrolState);
            }
        }

        public void Exit()
        {
            
        }
    }
}