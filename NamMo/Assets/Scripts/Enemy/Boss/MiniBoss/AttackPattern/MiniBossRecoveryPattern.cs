using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossRecoveryPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.HealthRecovery();

            yield return new WaitForSeconds(2f);
            
            _gameObject.EndHealthRecovery();
            _gameObject.TransitionToIdel();
            

        }
    }
}