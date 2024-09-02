using Enemy.MelEnemy;
using UnityEngine;

namespace Enemy.Boss.MiniBoss.State
{
    public class DashAttackState: IStateClass
    {
        public MiniBossEnemy _MiniBossEnemy;

        private Vector2 target;
        public DashAttackState(MiniBossEnemy _miniBossEnemy)
        {
            _MiniBossEnemy = _miniBossEnemy;
        }
        
        public void Enter()
        {
            _MiniBossEnemy._isAttacking = true;
            _MiniBossEnemy.DashAttackPatternStart();
            _MiniBossEnemy.GroggyEnter();
            
            target = Managers.Scene.CurrentScene.Player.transform.position;
        }

        public void Update()
        {
            if (!_MiniBossEnemy._isAttacking)
            {
                _MiniBossEnemy._miniBossStateMachine.TransitionState(_MiniBossEnemy._miniBossStateMachine.TurmState);
            }
            
            Vector2 _curr = _MiniBossEnemy.gameObject.transform.position;
            _curr.x = Mathf.MoveTowards(_curr.x, target.x, 6 * Time.deltaTime);
            _MiniBossEnemy.transform.position = _curr;
            
        }

        public void Exit()
        {
            
        }
    }
}