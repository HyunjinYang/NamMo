using System.Collections;
using UnityEngine;

namespace Enemy.MelEnemy
{
    public class MelEnemyBaseAttack : MelEnemyAttackPattern<MelEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.Onattack.Invoke();
            Debug.Log(_gameObject.Attack1Time1);
            yield return new WaitForSeconds(_gameObject.Attack1Time1);
            _gameObject._enemyAttack1BlockArea.ActiveBlockArea();
            yield return new WaitForSeconds(_gameObject.Attack1Time2);
            _gameObject._enemyAttack2BlockArea.ActiveBlockArea();
            yield return new WaitForSeconds(0.2f);
            _gameObject.OnEndattack.Invoke();
        }
    }
}