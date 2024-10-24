using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossAxeAttackPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.AxeAttack();

            yield return new WaitForSeconds(_gameObject.attack4Time);
            _gameObject.EnemyAxeAttackArea.Attack();
            yield return new WaitForFixedUpdate();

            _gameObject.EndAxeAttack();
        }
    }
}