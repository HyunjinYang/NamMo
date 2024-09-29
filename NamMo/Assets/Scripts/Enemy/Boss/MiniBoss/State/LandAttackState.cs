using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class LandAttackState : IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        public LandAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }

        public void Enter()
        {
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.transform.position = new Vector2(Managers.Scene.CurrentScene.Player.transform.position.x , _MiniBossEnemy.transform.position.y);
            _MiniBossEnemy.LandAttackPatternStart();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}