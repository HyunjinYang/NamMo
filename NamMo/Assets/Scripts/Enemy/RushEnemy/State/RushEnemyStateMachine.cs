using Enemy.MelEnemy;

namespace Enemy.RushEnemy.State
{
    public class RushEnemyStateMachine
    {
        public IStateClass _CurrentState;

        public IdelState _IdelState;
        public PatrolState _PatrolState;
        public RushAttackState _RushAttackState;
        public HitState _HitState;
        public TurmState _TurmState;
        public GroggyState _GroggyState;
        public JumpState _JumpState;
        public DeadState _DeadState;
        public RushEnemyStateMachine(RushEnemy _enemy)
        {
            _IdelState = new IdelState(_enemy);
            _PatrolState = new PatrolState(_enemy);
            _RushAttackState = new RushAttackState(_enemy);
            _HitState = new HitState(_enemy);
            _TurmState = new TurmState(_enemy);
            _GroggyState = new GroggyState(_enemy);
            _JumpState = new JumpState(_enemy);
            _DeadState = new DeadState(_enemy);
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