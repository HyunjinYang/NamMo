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
            yield return new WaitForSeconds(_gameObject.melAttack2Time);
            _gameObject.EnemyMelAttack2AttackArea.ActiveAttackArea();

            if (_gameObject._isMelAttack == 1)
            {
                Debug.Log("Attack3!!");
                yield return new WaitForSeconds(_gameObject.melAttack3Time);
                _gameObject.EnemyMelAttack3AttackArea.ActiveAttackArea();
            }
            
            _gameObject.EndMelAttack();
        }
    }
}