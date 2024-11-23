using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTree_Enemy
{
    public class ExitAnimationAction: EnemyAction
    {
        public string _animTriggerName;
        public override void OnStart()
        {
            _animator.SetBool(_animTriggerName, false);   
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}