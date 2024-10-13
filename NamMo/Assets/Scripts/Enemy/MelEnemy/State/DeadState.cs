namespace Enemy.MelEnemy
{
    public class DeadState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public DeadState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        public void Enter()
        {
            _MelEnemy.EndAttack();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }   
    }
}