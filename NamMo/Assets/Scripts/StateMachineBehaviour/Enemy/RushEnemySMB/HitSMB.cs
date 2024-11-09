using Enemy.RushEnemy;
using UnityEngine;
using UnityEngine.Animations;

namespace NamMo.Enemy.RushEnemySMB
{
    public class HitSMB: SceneLinkedSMB<RushEnemy>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.TransitionToHit();
        }

        public override void OnSLStatePostEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex) {
            
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            
        }
        public override void OnSLStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.TransitionEndHit();
        }
    }
}