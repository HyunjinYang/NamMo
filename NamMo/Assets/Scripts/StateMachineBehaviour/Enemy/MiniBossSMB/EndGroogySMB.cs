using Enemy.Boss.MiniBoss;
using UnityEngine;
using UnityEngine.Animations;

namespace NamMo.Enemy.MiniBossSMB
{
    public class EndGroogySMB: SceneLinkedSMB<MiniBossEnemy>
    {
        public override void OnSLStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.OnEndGroggy();
            _monoBehaviour.TransitionToIdel();
        } 
    }
}