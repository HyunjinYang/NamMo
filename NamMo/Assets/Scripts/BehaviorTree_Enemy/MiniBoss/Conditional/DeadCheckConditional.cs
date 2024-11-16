using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTree_Enemy.MiniBoss.Conditional
{
    public class DeadCheckConditional: EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            return _enemy.GetHp() <= 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}