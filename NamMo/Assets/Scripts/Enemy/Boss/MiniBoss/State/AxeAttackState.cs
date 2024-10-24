using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class AxeAttackState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public AxeAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.AxeAttackPatternStart();
        }

        public void Update()
        {
            if (!_MiniBossEnemy._isAttacking)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.TurmState);
            }
        }

        public void Exit()
        {
            
        }
    }
}