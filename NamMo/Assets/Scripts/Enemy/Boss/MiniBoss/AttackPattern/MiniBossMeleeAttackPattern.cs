using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossMeleeAttackPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.MelAttack();

            yield return new WaitForSeconds(_gameObject.melAttack1Time);
            _gameObject.EnemyMelAttack1AttackArea.Attack();
            yield return new WaitForFixedUpdate();
            
            
            yield return new WaitForSeconds(_gameObject.melAttack2Time);
            
            _gameObject.EnemyMelAttack2AttackArea.Attack();
            yield return new WaitForFixedUpdate();
            
            if (_gameObject._isMelAttack == 1)
            {
                Debug.Log("Attack3!!");
                yield return new WaitForSeconds(_gameObject.melAttack3Time);
                _gameObject.EnemyMelAttack3AttackArea.Attack();
                yield return new WaitForFixedUpdate();

                if (_gameObject.phase == 2)
                {
                    yield return new WaitForSeconds(0.9f);
                    _gameObject.ShootWave();
                }
            }

            yield return new WaitForSeconds(0.35f);
            
            _gameObject.EndMelAttack();
        }
    }
}