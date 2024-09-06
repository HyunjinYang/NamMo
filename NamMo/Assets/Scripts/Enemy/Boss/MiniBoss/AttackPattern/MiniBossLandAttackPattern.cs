using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossLandAttackPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.LandAttack();

            yield return new WaitForSeconds(_gameObject.landAttackTime);
            
            _gameObject.ShootWave();

            yield return new WaitForSeconds(0.5f);
            
            _gameObject.EndLandAttack();
        }
    }
}