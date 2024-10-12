using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class DeadState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public DeadState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Dead State");   

        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}