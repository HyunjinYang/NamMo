using System;
using UnityEngine;

namespace Enemy.Boss.MiniBoss
{
    public class MiniBossSpwn: MonoBehaviour
    {
        [SerializeField] private Transform _spwnPoint;
        [SerializeField] private GameObject _MiniBoss;
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                var cur =Instantiate(_MiniBoss, _spwnPoint.position, _spwnPoint.rotation);
                Destroy(this);
            }
        }
    }
}