using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class HitState: IStateClass
    {
        public RangedEnemy _RangedEnemy;
        
        public HitState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy Hit State");   

        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _RangedEnemy.EndHit();
        }
    }
}