namespace Enemy.MelEnemy
{
    public class StateMachine
    {
        public IStateClass _CurrentState;

        public PatrolState _patrolstate;
        public AttackState _AttackState;
        public IdelState _IdelState;
        public TraceState _TraceState;
        public HitState _HitState;
        public GroggyState _GroggyState;
        public DeadState _DeadState;
        public TurmState _TurmState;
        public StateMachine(MelEnemy _enemy)
        {
            _patrolstate = new PatrolState(_enemy);
            _AttackState = new AttackState(_enemy);
            _IdelState = new IdelState(_enemy);
            _TraceState = new TraceState(_enemy);
            _HitState = new HitState(_enemy);
            _GroggyState = new GroggyState(_enemy);
            _DeadState = new DeadState(_enemy);
            _TurmState = new TurmState(_enemy);
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