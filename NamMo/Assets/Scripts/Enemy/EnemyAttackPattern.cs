using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackPattern<TMonoBehaviour> : MonoBehaviour
        where TMonoBehaviour : Enemy 
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