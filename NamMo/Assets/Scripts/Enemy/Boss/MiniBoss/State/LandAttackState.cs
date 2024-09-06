using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class LandAttackState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public LandAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.LandAttackPatternStart();
            _MiniBossEnemy.GroggyEnter();
            
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