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
            _gameObject.EnemyMelAttack1AttackArea.ActiveAttackArea();
            yield return new WaitForFixedUpdate();
            
            _gameObject.EnemyMelAttack1AttackArea.DeActiveAttackArea();
            
            yield return new WaitForSeconds(_gameObject.melAttack2Time);
            
            _gameObject.EnemyMelAttack2AttackArea.ActiveAttackArea();
            yield return new WaitForFixedUpdate();
            _gameObject.EnemyMelAttack2AttackArea.DeActiveAttackArea();
            
            if (_gameObject._isMelAttack == 1)
            {
                Debug.Log("Attack3!!");
                yield return new WaitForSeconds(_gameObject.melAttack3Time);
                _gameObject.EnemyMelAttack3AttackArea.ActiveAttackArea();
                yield return new WaitForFixedUpdate();
                _gameObject.EnemyMelAttack3AttackArea.DeActiveAttackArea();
            }
            
            _gameObject.EndMelAttack();
        }
    }
}