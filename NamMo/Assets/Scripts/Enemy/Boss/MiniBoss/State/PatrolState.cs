using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class PatrolState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public PatrolState(MiniBossEnemy _miniBossEnemy)
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