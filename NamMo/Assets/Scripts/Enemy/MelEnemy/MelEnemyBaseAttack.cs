using System.Collections;
using UnityEngine;

namespace Enemy.MelEnemy
{
    public class MelEnemyBaseAttack : MelEnemyAttackPattern<MelEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.Onattack.Invoke();
            yield return new WaitForSeconds(_gameObject.Attack1Time1);
            
            if (!_gameObject._isAttacking)
                yield break;
            
            _gameObject.EnemyAttack1AttackArea.ActiveAttackArea();
            yield return new WaitForSeconds(_gameObject.Attack1Time2);
            
            if (!_gameObject._isAttacking)
                yield break;
            
            _gameObject.EnemyAttack2AttackArea.ActiveAttackArea();
            yield return new WaitForSeconds(0.2f);
            _gameObject.OnEndattack.Invoke();
        }
    }
}