using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTree_Enemy
{
    public class EnterAnimationAction: EnemyAction
    {
        public string _animTriggerName;
        public override void OnStart()
        {
            _animator.SetBool(_animTriggerName, true);   
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}