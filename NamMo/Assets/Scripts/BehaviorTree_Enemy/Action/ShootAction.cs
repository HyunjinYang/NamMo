using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class ShootAction: EnemyAction
    {
        public GameObject _projectile;
        public override void OnStart()
        {
            int direction = Managers.Scene.CurrentScene.Player.transform.position.x < transform.position.x ? -1 : 1;

            var projectile = Object.Instantiate(_projectile, new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 0.5f, _enemy.transform.position.z), transform.rotation);
            projectile.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 1f, 1, 4, Managers.Scene.CurrentScene.Player.gameObject);
            projectile.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (projectile)
                {
                    Managers.Resource.Destroy(projectile);
                }
            });

        }
    }
}