using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MinBossLandAttackPattern : MiniBossAttackPattern<MiniBossEnemy>
    {
        public override IEnumerator Pattern()
        {
            
            
            yield return  new WaitForSeconds(_gameObject.landAttackTime);
        }
    }
}