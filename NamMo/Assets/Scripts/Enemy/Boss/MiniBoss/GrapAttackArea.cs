using System;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class GrapAttackArea : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _radius;
        private Collider2D[] _hits;
        private MiniBossEnemy _attacker;

        public void SetInfo(MiniBossEnemy _attacker)
        {
            this._attacker = _attacker;
        }
        public void SetAttackRange(Vector2 offset, Vector2 size)
        {
            _offset = offset;
            _size = size;
        }
        bool _isAttackerFacingRight = false;
        
        
        protected void UpdateAttack()
        {
            _isAttackerFacingRight = _attacker.GetComponent<Enemy>().IsFacingRight;
        }

        public void Attack()
        {
            Vector2 offset = _offset;
            if (!_isAttackerFacingRight) offset.x = -_offset.x;
            
            _hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + offset, _size, 0);
            bool res = Managers.Scene.CurrentScene.Player.GetASC()
                .IsExsistTag(Define.GameplayTag.Player_Action_Parrying);

        }

        public void Update()
        {
            UpdateAttack();
        }
    }
}