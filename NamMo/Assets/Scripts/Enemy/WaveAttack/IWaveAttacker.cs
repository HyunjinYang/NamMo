using UnityEngine;

namespace Enemy.WaveAttack
{
    public interface IWaveAttacker
    {
        public void WaveParried();
        public Transform GetPosition();
    }
}