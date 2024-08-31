namespace Enemy.MelEnemy
{
    public class HitState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public HitState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        
        public void Enter()
        {
            
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _MelEnemy.EndHit();
        }
    }
}