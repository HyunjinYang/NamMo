using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class BlockState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public BlockState(ShieldEnemy _shieldEnemy)
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