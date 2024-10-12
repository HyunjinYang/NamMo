using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.State
{
    public class RangeAttackState: IStateClass
    {
        public RangedEnemy _RangedEnemy;

        public RangeAttackState(RangedEnemy _rangedEnemy)
        {
            _RangedEnemy = _rangedEnemy;
        }
        
        public void Enter()
        {
            Debug.Log("RangeEnemy RangeAttack State");   
            _RangedEnemy._isAttacking = true;
            _RangedEnemy.RangeAttackPatternStart();
        }

        public void Update()
        {
            if (!_RangedEnemy._isAttacking)
            {
                _RangedEnemy.stateMachine.TransitionState(_RangedEnemy.stateMachine._TurmState);
            }
        }
        public void Exit()
        {
            
        }
    }
}