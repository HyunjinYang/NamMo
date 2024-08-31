using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class IdelState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;
        
        public IdelState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            
        }

        public void Update()
        {
            if (_MiniBossEnemy._distance <= 3.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.MeleeAttackState);
            }
            
            
        }

        public void Exit()
        {
            
        }
    }
}