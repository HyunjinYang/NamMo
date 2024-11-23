using BehaviorDesigner.Runtime.Tasks;
using Enemy;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class AttackTrigger: EnemyAction
    {
        public EnemyAttackArea _EnemyAttackArea;
        public float _GroggyCount;
        public override void OnStart()
        {
            _enemy._EnemyState = Define.EnemyState.Attack;
            _enemy.SetGroggyCount(_GroggyCount);
            _EnemyAttackArea.SetAttackInfo(_enemy.gameObject, 2);
            _EnemyAttackArea._groggy = _enemy.OnGroggy;
            _EnemyAttackArea.Attack();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
            
        }
    }
}