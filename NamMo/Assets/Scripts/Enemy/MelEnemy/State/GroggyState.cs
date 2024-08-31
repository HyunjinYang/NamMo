namespace Enemy.MelEnemy
{
    public class GroggyState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public GroggyState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        public void Enter()
        {
            _MelEnemy.Groggy();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _MelEnemy.EndGroggy();
        }
    }
}