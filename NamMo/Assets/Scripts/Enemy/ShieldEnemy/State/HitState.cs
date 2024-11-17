using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class HitState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public HitState(ShieldEnemy _shieldEnemy)
        {
            ShieldEnemy = _shieldEnemy;
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