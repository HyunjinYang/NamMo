using System;
using UnityEngine;

namespace Wave
{
    public class WaveFx: MonoBehaviour
    {
        public Transform fixedSpawnPosition; // Your desired spawn point
        private ParticleSystem ps;
        private ParticleSystem.Particle[] particles;
        private float timer;
        void Start()
        {
            ps = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                //PosUpdate();
                timer = 0;
            }
            
        }

        void LateUpdate()
        {
            int numParticlesAlive = ps.GetParticles(particles);
            Vector3 pos = fixedSpawnPosition.position;
            for (int i = 0; i < numParticlesAlive; i++)
            {
                if(particles[i].GetCurrentSize(ps) < 0.1f) 
                    particles[i].position = pos;
                // Set the position of each particle to a fixed spawn point
            }

            // Apply changes back to the particle system
            ps.SetParticles(particles, numParticlesAlive);
        }
    }
}