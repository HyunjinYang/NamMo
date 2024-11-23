using System.Numerics;
using BehaviorDesigner.Runtime.Tasks;
using Vector2 = UnityEngine.Vector2;

namespace BehaviorTree_Enemy.MiniBoss.Conditional
{
    public class GroggyCheckConditional: EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            if (_enemy.CurrentAttackCount == 0 && _enemy.currentgroggyStet >= _enemy.maxGroggyStet)
            {
                _enemy._EnemyState = Define.EnemyState.Groggy;
                return TaskStatus.Success;
            }
            else
                return TaskStatus.Failure;
        }
    }
}