using Enemy.MelEnemy;
using Unity.VisualScripting;
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
            _MiniBossEnemy.StartIdelTurm();
            Debug.Log("IdelState!");
        }

        public void Update()
        {
            if (_MiniBossEnemy.idelTurm)
                return;
            
            if (_MiniBossEnemy.IsDead())
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine._DeadState);   
            }
            else if (_MiniBossEnemy.HP < 7 && _MiniBossEnemy.HealSelect())
            {
                
            }
            else
            {
                if (_MiniBossEnemy._distance <= 5.5f)
                {
                    _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine
                        .MeleeAttackState);
                }
                else if (_MiniBossEnemy._distance > 8.5f && _MiniBossEnemy._distance <= 13.5f)
                {
                    _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine
                        ._DashAttackState);
                }
                else if (_MiniBossEnemy._distance > 13.5f)
                {
                    _MiniBossEnemy.PatternSelect();
                }
                
            }
        }

        public void Exit()
        {
            _MiniBossEnemy.idelTurm = true;
        }
    }
}