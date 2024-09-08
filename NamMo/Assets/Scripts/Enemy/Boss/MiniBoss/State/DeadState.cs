using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class DeadState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public DeadState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}