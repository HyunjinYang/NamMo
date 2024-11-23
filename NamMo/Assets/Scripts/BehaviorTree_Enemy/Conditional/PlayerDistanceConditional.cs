using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class PlayerDistanceConditional: EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            return Vector2.Distance(_enemy.transform.position,
                Managers.Scene.CurrentScene.Player.transform.position) <= 3.5f
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}