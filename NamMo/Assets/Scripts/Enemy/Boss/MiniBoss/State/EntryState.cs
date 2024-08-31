using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class EntryState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public EntryState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
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