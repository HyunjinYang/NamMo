using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class GroggyState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public GroggyState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Groggy State");
            _RangedEnemy.StopPattern();
   
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}