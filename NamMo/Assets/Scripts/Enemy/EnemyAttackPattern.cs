using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackPattern<TMonoBehaviour> : MonoBehaviour
        where TMonoBehaviour : Enemy 
    {
        protected TMonoBehaviour _gameObject;
        public int AttackCount;
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