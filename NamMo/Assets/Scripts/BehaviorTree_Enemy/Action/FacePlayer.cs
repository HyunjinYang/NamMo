using System.Threading.Tasks;
using TaskStatus = BehaviorDesigner.Runtime.Tasks.TaskStatus;

namespace BehaviorTree_Enemy
{
    public class FacePlayer: EnemyAction
    {
        private float basescale;
        public override void OnStart()
        {
            basescale = transform.localScale.x;
        }

        public override TaskStatus OnUpdate()
        {
            var scale = transform.localScale;
            if (transform.position.x > Managers.Scene.CurrentScene.Player.transform.position.x && basescale > 0)
                scale.x = -basescale;
            else if (transform.position.x < Managers.Scene.CurrentScene.Player.transform.position.x && basescale < 0)
                scale.x = -basescale;
            transform.localScale = scale;
            return TaskStatus.Success;
        }
    }
}