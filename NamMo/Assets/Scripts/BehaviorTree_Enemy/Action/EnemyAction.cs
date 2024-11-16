using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace BehaviorTree_Enemy
{
    public class EnemyAction: Action
    {
        protected Rigidbody2D _rigidbody2D;
        protected BoxCollider2D _boxCollider2D;
        protected Animator _animator;
        protected TestEnemy _enemy;

        public override void OnAwake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            _animator = gameObject.GetComponentInChildren<Animator>();
            _enemy = gameObject.GetComponentInChildren<TestEnemy>();
        }
    }
}