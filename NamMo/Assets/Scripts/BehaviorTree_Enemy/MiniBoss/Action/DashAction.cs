using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree_Enemy.MiniBoss
{
    public class DashAction: EnemyAction
    {
        public bool hasAttack;
        public float dashTime;
        public string animationTriggerName;
        public float speed;
        private int direction;
        private Vector2 target;
        public override void OnStart()
        {
            hasAttack = false;
            _animator.SetTrigger(animationTriggerName);
            StartDash();
        }

        private void StartDash()
        {
            direction = Managers.Scene.CurrentScene.Player.transform.position.x < transform.position.x ? 1 : -1;
            target = new Vector2(Managers.Scene.CurrentScene.Player.transform.position.x + (4.5f * direction), transform.position.y);
            DOVirtual.DelayedCall(dashTime, () =>
            {
                hasAttack = true;
                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f);
            }, false);
        }        
        public override TaskStatus OnUpdate()
        {
            Vector2 _curr = gameObject.transform.position;
            _curr.x = Mathf.MoveTowards(_curr.x, target.x, speed* Time.deltaTime);
            transform.position = _curr;
            
            return hasAttack ? TaskStatus.Success: TaskStatus.Running;
        }
    }
}