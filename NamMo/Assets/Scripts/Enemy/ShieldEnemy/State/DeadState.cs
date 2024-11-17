using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class DeadState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public DeadState(ShieldEnemy shieldEnemy)
        {
            ShieldEnemy = shieldEnemy;
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