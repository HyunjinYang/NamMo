using BehaviorDesigner.Runtime.Tasks;
using Contents.Test;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class ShootAction : EnemyAction
    {
        public GameObject _projectile;
        public float _horizionForce;
        public float _verticalForce;
        
        public override void OnStart()
        {
            var projectile = Object.Instantiate(_projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            projectile.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 3);
            projectile.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (projectile)
                {
                    Managers.Resource.Destroy(projectile);
                }
            });
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}