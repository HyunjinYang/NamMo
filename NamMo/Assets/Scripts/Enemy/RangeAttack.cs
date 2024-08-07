using System;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace Enemy
{
    public class RangeAttack : EnemyAttack
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 target;
        private void Awake()
        {
            target = Managers.Gamemanager.ReturnToPlayerPostion();
        }

        private void FixedUpdate()
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                target, _speed * Time.deltaTime);
            
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