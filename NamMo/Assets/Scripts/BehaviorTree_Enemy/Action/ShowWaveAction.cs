using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class ShowWaveAction: EnemyAction
    {
        public override void OnStart()
        {
            if(Vector2.Distance(_enemy.gameObject.transform.position, 
                   Managers.Scene.CurrentScene.Player.transform.position) > 3.5f)
                _enemy.ShowWaveVFX();
        }
    }
}