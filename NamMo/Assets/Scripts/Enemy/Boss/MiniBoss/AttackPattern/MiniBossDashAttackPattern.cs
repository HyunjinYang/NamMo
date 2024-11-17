using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossDashAttackPattern: EnemyAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.DashAttack();

            yield return new WaitForSeconds(_gameObject.dashAttackTime);
            
            _gameObject.EnemyDashAttackAttackArea.Attack();

            yield return new WaitForFixedUpdate();
            

            if (_gameObject.phase == 2)
            {
                yield return new WaitForSeconds(0.3f);
                _gameObject.ShootWave();
            }
            _gameObject.EndDashAttack();

        }
    }
}   