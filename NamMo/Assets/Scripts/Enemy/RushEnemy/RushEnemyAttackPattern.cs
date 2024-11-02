using System.Collections;
using UnityEngine;

namespace Enemy.RushEnemy
{
    public class RushEnemyAttackPattern<TMonoBehaviour> : MonoBehaviour
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