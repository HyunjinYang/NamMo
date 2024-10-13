using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossGrapAttackPattern: MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            _gameObject.OnGrapAttack.Invoke();
            
            yield return new WaitForSeconds(_gameObject.grapAttackTime);
            
            //_gameObject.EnemyGrapAttackArea.Attack();

            yield return new WaitForFixedUpdate();

            if (!Managers.Scene.CurrentScene.Player.GetASC()
                    .IsExsistTag(Define.GameplayTag.Player_Action_Parrying))
            {
                
            }

        }
    }
}