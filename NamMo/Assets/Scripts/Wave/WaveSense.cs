using UnityEngine;
using System;
using Unity.VisualScripting;

namespace Wave
{
    public class WaveSense: MonoBehaviour
    {
        private void FixedUpdate()
        {
            Debug.Log("ASD");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.GetComponent<WaveHitObject>() != null)
            {
                collision.gameObject.GetComponent<WaveHitObject>().Activate();
            }
        }
    }
}