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
            _gameObject._enemyAttack3BlockArea.ActiveBlockArea();
            _gameObject.OnEndDownAttack.Invoke();
        }
    }
}