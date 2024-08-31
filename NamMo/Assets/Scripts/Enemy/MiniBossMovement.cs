using UnityEngine;

namespace Enemy
{
    public class MiniBossMovement: EnemyMovement
    {
        protected override void FixedUpdate()
        {
            if (_isHit || _isDead)
                return;

            if (_isPatrol && !_isAttack)
            {
                if ((_CharacterSprite.transform.position.x < _point1.position.x ||
                     _CharacterSprite.transform.position.x > _point2.position.x) 
                    && (_playerposition.position.x < _point1.position.x || _playerposition.position.x > _point2.position.x))
                {
                    OnWalk.Invoke(0f);
                    return;
                }

                OnWalk.Invoke(1f);
                DirectCheck(_CharacterSprite.transform.position.x, _playerposition.position.x);
                Vector2 _curr = _CharacterSprite.transform.position;
                _curr.x = Mathf.MoveTowards(_curr.x, _playerposition.position.x, _speed * Time.deltaTime);
                _CharacterSprite.transform.position = _curr;
            }
        }
    }
}