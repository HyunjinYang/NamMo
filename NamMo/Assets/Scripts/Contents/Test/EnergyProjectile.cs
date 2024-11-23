using UnityEngine;

namespace Contents.Test
{
    public class EnergyProjectile: BaseProjectile, IParryingable
    {
        [SerializeField] Vector2 force;
        [SerializeField] private float speedd;
        private float basescale;

        public override void SetAttackInfo(GameObject attacker, float damage, int attackStrength = 1, float speed =0, GameObject target = null)
        {
            
            base.SetAttackInfo(attacker, damage, attackStrength, speed, target);
            _target = target;
            basescale = transform.localScale.x;
            var scale = transform.localScale;
            scale.x = transform.position.x > Managers.Scene.CurrentScene.Player.transform.position.x
                ? -basescale
                : basescale;
            transform.localScale = scale;
            //speedd = speed;
        }

        protected override void UpdateAttack()
        {
            gameObject.transform.position += new Vector3(force.x, force.y) * speedd* Time.deltaTime;
        }
        public void Force(int direction)
        {
            force.x *= direction;
        }

        public void Parried(GameObject attacker, GameObject target = null)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}