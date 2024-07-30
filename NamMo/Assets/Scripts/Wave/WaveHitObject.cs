using UnityEngine;
using System;
using System.Collections;

namespace Wave
{
    public class WaveHitObject: MonoBehaviour
    {
        [SerializeField] private GameObject ObjectOutLine;
        
        public void Activate()
        {
            StartCoroutine(HitWave());
        }

        IEnumerator HitWave()
        {
            ObjectOutLine.SetActive(true);

            yield return new WaitForSeconds(5f);
            
            ObjectOutLine.SetActive(false);
        }
    }
}