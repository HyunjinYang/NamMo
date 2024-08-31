using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossDashAttackPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.DashAttack();

            yield return new WaitForSeconds(_gameObject.dashAttackTime);
            
            _gameObject._enemyDashAttackBlockArea.ActiveAttackArea();
            
        }
    }
}