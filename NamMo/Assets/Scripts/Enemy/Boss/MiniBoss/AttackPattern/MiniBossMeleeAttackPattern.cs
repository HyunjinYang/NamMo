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
            _gameObject._enemyMelAttack1BlockArea.ActiveAttackArea();
            yield return new WaitForSeconds(_gameObject.melAttack2Time);
            _gameObject._enemyMelAttack2BlockArea.ActiveAttackArea();

            if (_gameObject._isMelAttack == 1)
            {
                yield return new WaitForSeconds(_gameObject.melAttack3Time);
                _gameObject._enemyMelAttack3BlockArea.ActiveAttackArea();
            }
            
            _gameObject.EndMelAttack();
        }
    }
}