using UnityEngine;

namespace Enemy
{
    public class EnemyBlockArea : BlockArea
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>() == null) 
                return;
            
            Debug.Log("플레이어 공격!");
        }
        
        
    }
}