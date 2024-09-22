using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class ChangePhaseState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public ChangePhaseState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        public void Enter()
        {
            _MiniBossEnemy._isinvincibility = true;
            _MiniBossEnemy.ChangePhase();
            _MiniBossEnemy.phase = 2;
            _MiniBossEnemy.HP = 5;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _MiniBossEnemy.EndChangePhase();
            _MiniBossEnemy._isinvincibility = false;
        }
    }
}