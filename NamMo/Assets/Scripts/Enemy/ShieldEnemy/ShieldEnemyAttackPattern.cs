using System.Collections;
using UnityEngine;

namespace Enemy.ShieldEnemy
{
    public class ShieldEnemyAttackPattern<TMonoBehaviour> : MonoBehaviour
        where TMonoBehaviour : MonoBehaviour 
    {
        protected TMonoBehaviour _gameObject;

        public void Initalize(TMonoBehaviour gameobject)
        {
            _gameObject = gameobject;
        }

        public virtual IEnumerator Pattern()
        {
            return null;
        }        
    }
}