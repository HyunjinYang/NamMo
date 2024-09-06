using Enemy.MelEnemy;

namespace Enemy.Boss.MiniBoss.State
{
    public class MiniBossStateMachine
    {
        public IStateClass _CurrentState;

        public EntryState _EntryState;
        public PatrolState _PatrolState;
        public MeleeAttackState MeleeAttackState;
        public GroggyState _GroggyState;
        public IdelState IdelState;
        public TurmState TurmState;
        public DashAttackState _DashAttackState;
        public LandAttackState _LandAttackState;
        
        public MiniBossStateMachine(MiniBossEnemy _miniBossEnemy)
        {
            _EntryState = new EntryState(_miniBossEnemy);
            _PatrolState = new PatrolState(_miniBossEnemy);
            MeleeAttackState = new MeleeAttackState(_miniBossEnemy);
            _GroggyState = new GroggyState(_miniBossEnemy);
            IdelState = new IdelState(_miniBossEnemy);
            TurmState = new TurmState(_miniBossEnemy);
            _DashAttackState = new DashAttackState(_miniBossEnemy);
            _LandAttackState = new LandAttackState(_miniBossEnemy);
        }

        public void Update()
        {
            _CurrentState.Update();
        }

        public void Initalizze(IStateClass _stateClass)
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