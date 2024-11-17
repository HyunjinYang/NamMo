using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree_Enemy
{
    public class DeadAction: EnemyAction
    {
        public float bleedTime;

        public GameObject _DeadParticle;
        private bool isDead;
        public override void OnStart()
        {
            Managers.Effect.PlayOnShot(_DeadParticle, transform);
            DOVirtual.DelayedCall(bleedTime, () =>
            {
                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f);
                isDead = true;
                Object.Destroy(gameObject);
            }, false);
        }
    }
}   