using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class TargetToMove: EnemyAction
    {
        private bool hasEnter;
        public Transform _target;
        public override void OnStart()
        {
            if (_target == null)
                _target = Managers.Scene.CurrentScene.Player.transform;
            hasEnter = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector2.Distance(_target.position, _enemy.gameObject.transform.position) <= 1f && _target == null)
                hasEnter = true;
            else if (Vector2.Distance(_target.position, _enemy.gameObject.transform.position) <= 3.4f)
                hasEnter = true;
            _agent.SetDestination(_target.position);
            return hasEnter ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            _agent.ResetPath();
        }
    }
}