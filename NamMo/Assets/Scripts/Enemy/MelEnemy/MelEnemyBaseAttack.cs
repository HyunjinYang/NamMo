using System.Collections;
using UnityEngine;

namespace Enemy.MelEnemy
{
    public class MelEnemyBaseAttack : EnemyAttackPattern<MelEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.Onattack.Invoke();
            //_gameObject.ShowWaveVFX();
            yield return new WaitForSeconds(_gameObject.Attack1Time1);
            
            if (!_gameObject._isAttacking)
                yield break;

            if (_gameObject.ReturnIsDead())
                yield break;
            _gameObject.Direct();
            _gameObject.EnemyAttack1AttackArea.Attack();

            //yield return new WaitForFixedUpdate();
            
            yield return new WaitForSeconds(_gameObject.Attack1Time2);
            
            if (!_gameObject._isAttacking)
                yield break;
            
            _gameObject.EnemyAttack2AttackArea.Attack();
            yield return new WaitForFixedUpdate();
            _gameObject.OnEndattack.Invoke();
        }
    }
}