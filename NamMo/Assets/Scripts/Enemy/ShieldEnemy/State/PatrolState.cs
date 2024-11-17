using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class PatrolState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public PatrolState(ShieldEnemy _shieldEnemy)
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