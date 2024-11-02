using Enemy.MelEnemy;

namespace Enemy.RushEnemy.State
{
    public class DeadState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public DeadState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
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