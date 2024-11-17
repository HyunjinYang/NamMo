using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class AttackState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public AttackState(ShieldEnemy _shieldEnemy)
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