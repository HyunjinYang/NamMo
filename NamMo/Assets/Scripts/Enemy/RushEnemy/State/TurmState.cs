using Enemy.MelEnemy;

namespace Enemy.RushEnemy.State
{
    public class TurmState: IStateClass
    {
        public RushEnemy _RushEnemy;

        public TurmState(RushEnemy _rushEnemy)
        {
            _RushEnemy = _rushEnemy;
        }
        
        public void Enter()
        {
            _RushEnemy.StartTurm();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _RushEnemy.StopTurm();
        }
    }
}