namespace Enemy.MelEnemy
{
    public class TurmState : IStateClass
    {
        public MelEnemy _MelEnemy;

        public TurmState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        
        public void Enter()
        {
            _MelEnemy.StartTurm();   
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _MelEnemy.StopTurm();
        }
    }
}