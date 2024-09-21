using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class HealthRecoveryState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;
        public int chargeHp = 4;

        public HealthRecoveryState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            _MiniBossEnemy.RecoveryPatternStart();
            _MiniBossEnemy.HP += chargeHp;
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}