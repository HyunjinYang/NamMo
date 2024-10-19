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
            if (_gameObject.ReturnIsDead())
                yield break;
            _gameObject.Direct();
            _gameObject.EnemyAttack3AttackArea.Attack();
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.57f);
            _gameObject.OnEndDownAttack.Invoke();
        }
    }
}