using System.Collections;
using UnityEngine;

namespace Enemy.MelEnemy
{
    public class MelEnemyDownAttack: MelEnemyAttackPattern<MelEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.OnDownAttack.Invoke();
            yield return new WaitForSeconds(_gameObject.Attack2Time);
            
            if (!_gameObject._isAttacking)
                yield break;
            
            _gameObject.EnemyAttack3AttackArea.ActiveAttackArea();
            yield return new WaitForFixedUpdate();
            _gameObject.EnemyAttack3AttackArea.DeActiveAttackArea();
            yield return new WaitForSeconds(0.57f);
            _gameObject.OnEndDownAttack.Invoke();
        }
    }
}