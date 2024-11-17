using UnityEngine;

namespace Contents.Test
{
    public class EnergyProjectile: BaseProjectile, IParryingable
    {
        [SerializeField]Vector2 force;
        [SerializeField]private float speedd;
        public override void SetAttackInfo(GameObject attacker, float damage, int attackStrength = 1, float speed =0, GameObject target = null)
        {
            
            base.SetAttackInfo(attacker, damage, attackStrength, speed, target);
            _target = target;
            //speedd = speed;
        }

        protected override void UpdateAttack()
        {
            gameObject.transform.position += new Vector3(force.x, force.y) * speedd* Time.deltaTime;
        }
        public void Force(Vector2 vec2)
        {
            force = vec2;
        }

        public void Parried(GameObject attacker, GameObject target = null)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}