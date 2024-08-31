using Enemy.Boss.MiniBoss;
using UnityEngine;
using UnityEngine.Animations;

namespace NamMo.Enemy.MiniBossSMB
{
    public class MelAttackCombo2SMB: SceneLinkedSMB<MiniBossEnemy>
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
            _monoBehaviour._isAttacking = false;
        }
    }
}