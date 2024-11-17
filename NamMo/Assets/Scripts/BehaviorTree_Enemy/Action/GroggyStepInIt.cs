namespace BehaviorTree_Enemy
{
    public class GroggyStepInIt: EnemyAction
    {
        public override void OnStart()
        {
            _enemy.CurrentAttackCount = 0;
            _enemy.currentgroggyStet = 0;
        }
    }
}