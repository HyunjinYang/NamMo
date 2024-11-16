using System.Numerics;
using BehaviorDesigner.Runtime.Tasks;
using Vector2 = UnityEngine.Vector2;

namespace BehaviorTree_Enemy.MiniBoss.Conditional
{
    public class GroggyCheckConditional: EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            return _enemy.CurrentAttackCount == 0 && _enemy.currentgroggyStet >= _enemy.maxGroggyStet ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}