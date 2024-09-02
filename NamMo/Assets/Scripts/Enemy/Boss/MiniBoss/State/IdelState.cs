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
            Debug.Log("IdelState!");
        }

        public void Update()
        {
            if (_MiniBossEnemy._distance <= 3.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.MeleeAttackState);
            }
            
            else if (_MiniBossEnemy._distance > 3.5f && _MiniBossEnemy._distance <= 7.5f)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine._DashAttackState);
            }
            
            
        }

        public void Exit()
        {
            
        }
    }
}