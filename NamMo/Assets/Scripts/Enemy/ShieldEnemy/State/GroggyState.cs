using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class GroggyState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public GroggyState(ShieldEnemy _shieldEnemy)
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