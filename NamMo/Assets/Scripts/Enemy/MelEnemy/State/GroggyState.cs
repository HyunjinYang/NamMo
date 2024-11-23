using UnityEngine;

namespace Enemy.MelEnemy
{
    public class GroggyState: IStateClass
    {
        public MelEnemy _MelEnemy;

        public GroggyState(MelEnemy _melEnemy)
        {
            _MelEnemy = _melEnemy;
        }
        public void Enter()
        {
            Debug.Log("Groggy State");
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}