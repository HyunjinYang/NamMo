using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy
{
    public class TurmState: IStateClass
    {
        public ShieldEnemy ShieldEnemy;

        public TurmState(ShieldEnemy _shieldEnemy)
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