using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MinBossLandAttackPattern : MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.OnLandAttack.Invoke();
            
            yield return  new WaitForSeconds(_gameObject.landAttackTime);
            
            _gameObject.EnemyLandAttackArea.ActiveAttackArea();

            yield return new WaitForFixedUpdate();
            
            _gameObject.EnemyLandAttackArea.DeActiveAttackArea();

            yield return new WaitForSeconds(0.7f);

            
            _gameObject.OnEndLandAttack.Invoke();

            
            _gameObject._miniBossStateMachine.TransitionState(_gameObject._miniBossStateMachine.TurmState);

        }
    }
}