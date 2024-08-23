using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class RangeAttack : EnemyAttack
    {
        [SerializeField] private float _arcspeed;
        [SerializeField] private Vector2 target;
        private void Awake()
        {
            target = Managers.Gamemanager.ReturnToPlayerPostion();
        }

        private void FixedUpdate()
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                target, _arcspeed * Time.deltaTime);
            
            if(Vector2.Distance(gameObject.transform.position, target) <= 0f)
                Destroy(this);
        }

        /*
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<PlayerController>() != null)
            {
               Debug.Log("플레이어 공격!!");
               Destroy(this);
            }
        }*/
    }
}