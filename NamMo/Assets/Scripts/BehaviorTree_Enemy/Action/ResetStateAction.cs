namespace BehaviorTree_Enemy
{
    public class ResetStateAction: EnemyAction
    {
        public override void OnStart()
        {
            _enemy._EnemyState = Define.EnemyState.Default;
        }
    }
}