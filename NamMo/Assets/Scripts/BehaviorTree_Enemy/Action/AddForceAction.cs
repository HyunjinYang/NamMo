using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class AddForceAction: EnemyAction
    {
        public float horizontalForce;
        public float verticalForce;

        public float _actionTime;
        public bool hasExit;
        public override void OnStart()
        {
            StartAction();
        }

        private void StartAction()
        {
            var director = Managers.Scene.CurrentScene.Player.transform.position.x < transform.position.x ? 1 : -1;
            _rigidbody2D.AddForce(new Vector2(horizontalForce * director, verticalForce), ForceMode2D.Impulse);

            DOVirtual.DelayedCall(_actionTime, () =>
            {
                hasExit = true;
            }, false);
        }
        
        public override TaskStatus OnUpdate()
        {
            return hasExit ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}