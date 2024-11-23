namespace BehaviorTree_Enemy.MiniBoss.Action
{
    public class LandAction: EnemyAction
    {
        public override void OnStart()
        {
            transform.position = Managers.Scene.CurrentScene.Player.transform.position;
        }        
    }
}