using Enemy.MelEnemy;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Enemy.Boss.MiniBoss.State
{
    public class IdelState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;
        private bool _isHeal = false;
        public IdelState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            _MiniBossEnemy.StartIdelTurm();
            _isHeal = _MiniBossEnemy.HealSelect();
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
            else if (_MiniBossEnemy.HP < 24 && _isHeal)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine._HealthRecoveryState);
            }
            else
            {
                if (_MiniBossEnemy._distance <= 6.5f)
                {
                    Random rm = new Random();
                    var _lan = rm.Next(0, 9); 
                    Debug.Log(_lan);
                    if(_lan >= 0 && _lan <= 2)
                    {
                        _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine
                            ._AxeAttackState);   
                    }
                    else if(_lan >= 3 && _lan <= 5)
                    {
                        _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine
                            .MeleeAttackState);
                    }
                    else
                    {
                        _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine
                            ._DashAttackState);   
                    }
                }
                else if (_MiniBossEnemy._distance > 6.5f && _MiniBossEnemy._distance <= 13.5f)
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