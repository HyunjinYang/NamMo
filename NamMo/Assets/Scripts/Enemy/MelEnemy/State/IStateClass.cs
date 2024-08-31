using UnityEngine;

namespace Enemy.MelEnemy
{
    public interface IStateClass
    {
        public void Enter();

        public void Update();

        public void Exit();

    }
}