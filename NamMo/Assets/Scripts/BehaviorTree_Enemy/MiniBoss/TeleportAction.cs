using System;
using BehaviorDesigner.Runtime.Tasks;
using Contents.Scene;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree_Enemy.MiniBoss.Conditional
{
    public class TeleportAction: EnemyAction
    {
        public GameObject _teleportObject;

        private bool hasExit;
        public float teleportTime;
        
        public override void OnStart()
        {
            _teleportObject.SetActive(true);
            StartTeleport();
        }

        private void StartTeleport()
        {
            DOVirtual.DelayedCall(teleportTime, () =>
            {
                _teleportObject.SetActive(false);
                PositionTransition();
                hasExit = true;
            }, false);
        }
        
        private void PositionTransition()
        {
            if (Math.Abs(Vector2.Distance(gameObject.transform.position,
                    Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._leftPoint.position)) <
                Math.Abs(Vector2.Distance(gameObject.transform.position,
                    Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._rightPoint.position)))
            {
                gameObject.transform.position = Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._rightPoint.position;
            }
            else
            {
                gameObject.transform.position = Managers.Scene.CurrentScene.GetComponent<MiniBossScene>()._leftPoint.position;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return hasExit ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}