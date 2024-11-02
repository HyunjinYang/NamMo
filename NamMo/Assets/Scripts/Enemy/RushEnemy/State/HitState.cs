using Enemy.MelEnemy;

namespace Enemy.RushEnemy.State
{
    public class HitState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public HitState(RushEnemy _rushEnemy)
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
            _RushEnemy.EndHit();
        }
    }
}