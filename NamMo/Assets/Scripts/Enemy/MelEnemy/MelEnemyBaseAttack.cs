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

            yield return new WaitForSeconds(_gameObject.Attack1Time2);
        }
    }
}