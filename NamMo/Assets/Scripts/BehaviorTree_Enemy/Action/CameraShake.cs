using DG.Tweening;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class CameraShake: EnemyAction
    {
        public override void OnStart()
        {
            Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f);
        }
    }
}