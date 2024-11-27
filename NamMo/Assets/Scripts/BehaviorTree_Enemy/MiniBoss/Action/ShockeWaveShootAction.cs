using BehaviorDesigner.Runtime.Tasks;
using Contents.Test;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class ShockeWaveShootAction : EnemyAction
    {
        public GameObject _projectile;
        public float _horizionForce;
        public float _verticalForce;
        public override void OnStart()
        {
            int direction = Managers.Scene.CurrentScene.Player.transform.position.x < transform.position.x ? -1 : 1;

            var projectile = Object.Instantiate(_projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            projectile.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 3);
            projectile.GetComponent<EnergyProjectile>().Force(direction);
            projectile.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (projectile)
                {
                    Managers.Resource.Destroy(projectile);
                }
            });
            
            Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}