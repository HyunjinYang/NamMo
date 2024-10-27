using System.Collections;
using UnityEngine;

namespace Enemy.RushEnemy
{
    public class RushEnemyRushAttackPattern: RushEnemyAttackPattern<RushEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.Onattack.Invoke();
            
            _gameObject.Direction();
            yield return new WaitForSeconds(_gameObject.AttackTime);
            
            _gameObject.EnemyAttackArea.Attack();

            yield return new WaitForFixedUpdate();
            
            _gameObject.OnEndattack.Invoke();

        }
    }
}