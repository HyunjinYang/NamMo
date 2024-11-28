using System.Threading.Tasks;
using UnityEngine;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace BehaviorTree_Enemy
{
    public class FacePlayer: EnemyAction
    {
        private float basescale;
        public Transform _target;
        public override void OnStart()
        {
            basescale = _enemy.transform.localScale.x;
            if (_target == null)
            {
                _target = Managers.Scene.CurrentScene.Player.transform;
            }
        }

        public override TaskStatus OnUpdate()
        {
            var scale = _enemy.transform.localScale;
            if (_enemy.transform.position.x > _target.position.x && basescale > 0)
            {
                Debug.Log(_target.gameObject.name);
                scale.x = -basescale;
            }
            else if (_enemy.transform.position.x < _target.position.x && basescale < 0)
            {
                Debug.Log(_target.gameObject.name);

                scale.x = -basescale;
            }

            if (scale.x < 0)
                _enemy.IsFacingRight = true;
            else
                _enemy.IsFacingRight = false;
            _enemy.transform.localScale = scale;
            return TaskStatus.Success;
        }
    }
}