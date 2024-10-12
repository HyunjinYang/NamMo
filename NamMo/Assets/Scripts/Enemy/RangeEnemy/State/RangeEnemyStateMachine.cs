using Enemy.MelEnemy;

namespace Enemy.State
{
    public class RangeEnemyStateMachine
    {
        public IStateClass _CurrentState;

        public AttackState _AttackState;
        public DeadState _DeadState;
        public GroggyState _GroggyState;
        public HitState _HitState;
        public IdelState _IdelState;
        public PatrolState _PatrolState;
        public RangeAttackState _RangeAttackState;
        public TurmState _TurmState;
        
        public RangeEnemyStateMachine(RangedEnemy _enemy)
        {
            _AttackState = new AttackState(_enemy);
            _DeadState = new DeadState(_enemy);
            _GroggyState = new GroggyState(_enemy);
            _HitState = new HitState(_enemy);
            _IdelState = new IdelState(_enemy);
            _PatrolState = new PatrolState(_enemy);
            _RangeAttackState = new RangeAttackState(_enemy);
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