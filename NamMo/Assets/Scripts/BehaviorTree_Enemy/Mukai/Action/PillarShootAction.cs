
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTree_Enemy.Mukai
{
    public class PillarShootAction : EnemyAction
    {
        public GameObject pillar;
        public float dist;
        public float time;
        public override void OnStart()
        {
            int direction = Managers.Scene.CurrentScene.Player.transform.position.x < transform.position.x ? -1 : 1;
            dist *= direction;
            Quaternion quaternion = Quaternion.identity;
            quaternion.eulerAngles = new Vector3(0, 0, 45);
            var obj = Object.Instantiate(pillar,
                new Vector3(transform.position.x + dist, transform.position.y, transform.position.z), 
                quaternion);
            
            DOVirtual.DelayedCall(time, () =>
            {
                Camera.main.GetComponent<CameraController>().ShakeCamera(0.5f);
                
                if(obj)
                    Managers.Resource.Destroy(obj);
            }, false);
            

        }
    }
}