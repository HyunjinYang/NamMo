using Enemy.RushEnemy;
using UnityEngine;
using UnityEngine.Animations;

namespace NamMo.Enemy.RushEnemySMB
{
    public class GroggySMB: SceneLinkedSMB<RushEnemy>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.TransitionToGroggy();
        }

        public override void OnSLStatePostEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex) {
            
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            
        }
        public override void OnSLStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.OnEndGroggy.Invoke();
            if(!_monoBehaviour.ReturnIsHit())
                _monoBehaviour.TransitionEndGroggy();
        }
    }
}