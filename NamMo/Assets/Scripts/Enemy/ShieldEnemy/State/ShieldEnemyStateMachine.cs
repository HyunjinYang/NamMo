using Enemy.MelEnemy;

namespace Enemy.ShieldEnemy.State
{
    public class ShieldEnemyStateMachine
    {
        private IStateClass _CurrentState;

        public IdelState _IdelState;
        public PatrolState _PatrolState;
        public AttackState _AttackState;
        public HitState _HitState;
        public TurmState _TurmState;
        public GroggyState _GroggyState;
        public DeadState _DeadState;
        public BlockState _BlockState;

        public ShieldEnemyStateMachine(ShieldEnemy _enemy)
        {
            _IdelState = new IdelState(_enemy);
            _PatrolState = new PatrolState(_enemy);
            _AttackState = new AttackState(_enemy);
            _HitState = new HitState(_enemy);
            _TurmState = new TurmState(_enemy);
            _GroggyState = new GroggyState(_enemy);
            _DeadState = new DeadState(_enemy);
            _BlockState = new BlockState(_enemy);
        }

        public void Update()
        {
            _CurrentState.Update();
        }

        public void Initialize(IStateClass _stateClass)
        {
            _CurrentState = _stateClass;
            _stateClass.Enter();
        }

        public void TransitionState(IStateClass _stateClass)
        {
            _CurrentState.Exit();
            _CurrentState = _stateClass;
            _stateClass.Enter();
        }
    }
}