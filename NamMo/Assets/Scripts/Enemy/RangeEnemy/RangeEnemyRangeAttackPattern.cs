using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RangeEnemyRangeAttackPattern : RangeEnemyAttackPattern<RangedEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.RangeAttackAnim();
            
            yield return new WaitForSeconds(_gameObject._AttackTime2);
            
            _gameObject.CreateRangeAttack();
            _gameObject.EndRangeAttackAnim();
        }
    }
}