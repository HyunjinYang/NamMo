using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTree_Enemy
{
    public class SetTrigger: EnemyAction
    {
        public string _animTriggerName;
        public int AttackCount;
        public override void OnStart()
        {
            //base.OnAwake();
            _animator.SetTrigger(_animTriggerName);
            _enemy.CurrentAttackCount = AttackCount;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
            
        }
    }
}