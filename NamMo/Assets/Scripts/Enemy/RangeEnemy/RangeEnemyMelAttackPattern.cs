using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RangeEnemyMelAttackPattern : EnemyAttackPattern<RangedEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.MelAttackAnim();
            
            yield return new WaitForSeconds(_gameObject._AttackTime1);
            
            _gameObject._enemyAttackArea.Attack();

            _gameObject.EndMelAttackAnim();
        }
    }
}