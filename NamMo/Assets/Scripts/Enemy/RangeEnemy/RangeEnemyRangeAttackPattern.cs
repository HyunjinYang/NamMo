using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RangeEnemyRangeAttackPattern : EnemyAttackPattern<RangedEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.RangeAttackAnim();
            _gameObject.ShowWaveVFX();
            yield return new WaitForSeconds(_gameObject._AttackTime2);
            
            _gameObject.CreateRangeAttack();
            _gameObject.EndRangeAttackAnim();
        }
    }
}