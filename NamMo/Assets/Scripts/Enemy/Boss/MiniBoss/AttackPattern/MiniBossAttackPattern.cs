using System.Collections;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossAttackPattern<TMonoBehaviour> : MonoBehaviour
        where TMonoBehaviour : MonoBehaviour
    {
        protected TMonoBehaviour _gameObject;

        public void Initialise(TMonoBehaviour gameobject)
        {
            _gameObject = gameobject;
        }

        public virtual IEnumerator Pattern()
        {
            return null;
        }
    }
}