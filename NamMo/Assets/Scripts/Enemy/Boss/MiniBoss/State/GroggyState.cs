using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class GroggyState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public GroggyState(MiniBossEnemy _miniBossEnemy)
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