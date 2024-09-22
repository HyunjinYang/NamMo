using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossRecoveryPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject._teleport.SetActive(true);
            yield return new WaitForSeconds(2f);
            
            _gameObject.PositionTransition();
            
            yield return new WaitForSeconds(0.4f);
            
            _gameObject.HealthRecovery();

            yield return new WaitForSeconds(2f);
            _gameObject.EndHealthRecovery();
            _gameObject._teleport.SetActive(false);
            _gameObject.TransitionToIdel();
            

        }
    }
}