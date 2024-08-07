using UnityEngine;

namespace Enemy
{
    public class EnemyBlockArea : BlockArea
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == null) 
                return;
            
            collision.gameObject.GetComponent<PlayerController>().GetPlayerCombatComponent().GetDamaged(1f, gameObject.transform.position);
            Debug.Log("플레이어 공격!");
        }
        
        
    }
}