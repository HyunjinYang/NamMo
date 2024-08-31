namespace Enemy.MelEnemy
{
    public class IdelState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public IdelState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        
        public void Enter()
        {
            
        }

        public void Update()
        {
            if (_MelEnemy._distance <= 6f)
            {
                _MelEnemy.stateMachine.TransitionState(_MelEnemy.stateMachine._patrolstate);
                return;
            }
            
        }

        public void Exit()
        {
            
        }
    }
}