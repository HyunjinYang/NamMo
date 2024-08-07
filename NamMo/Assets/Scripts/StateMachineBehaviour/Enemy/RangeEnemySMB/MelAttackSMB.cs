using Enemy;
using UnityEngine;
using UnityEngine.Animations;
namespace NamMo
{
    public class MelAttackSMB: SceneLinkedSMB<RangedEnemy>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnSLStatePostEnter(Animator animator,AnimatorStateInfo stateInfo,int layerIndex) {
            _monoBehaviour.MelAttack();
            _monoBehaviour._isParingAvailable = true;
        }
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            
        }
        public override void OnSLStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.SetMelAttack(false);
            _monoBehaviour._isParingAvailable = false;
        }   
    }
}