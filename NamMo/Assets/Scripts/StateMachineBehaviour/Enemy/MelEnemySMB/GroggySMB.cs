using Enemy.MelEnemy;
using UnityEngine;
using UnityEngine.Animations;

namespace NamMo.Enemy.MelEnemySMB
{
    public class GroggySMB: SceneLinkedSMB<MelEnemy>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnSLStatePostEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex) {
            
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            
        }
        public override void OnSLStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.EndAttack();
            _monoBehaviour.OnEndGroggy();
        }
    }
}