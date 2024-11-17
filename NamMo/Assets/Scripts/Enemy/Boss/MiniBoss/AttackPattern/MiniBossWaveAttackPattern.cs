using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossWaveAttackPattern: EnemyAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.WaveAttack();

            yield return new WaitForSeconds(_gameObject.waveAttackTime);
            
            _gameObject.ShootWave();

            yield return new WaitForSeconds(0.5f);
            
            _gameObject.EndWaveAttack();
        }
    }
}