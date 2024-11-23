using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class PlayerFindConditional: EnemyConditional
    {
        public override void OnStart()
        {
            
        }

        public override TaskStatus OnUpdate()
        {
            return _enemy._isPlayerCheck  
                ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}