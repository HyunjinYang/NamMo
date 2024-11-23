using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        MainScene,
        PrologueScene,
        CaveScene,
        KatanaZeroScene1,
        KatanaZeroScene2,
        KatanaZeroScene3,
        KatanaZeroScene4,
        KatanaZeroScene1_KMW,
        KatanaZeroScene2_KMW,
        KatanaZeroScene3_KMW,
        KatanaZeroScene4_KMW,
        MiniBossScene,
        
        TestStartScene,

        MaxCount
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum KeyInput
    {
        W,
        S,
        A,
        D,
        Enter,
        Esc
    }

    public enum UIEvent
    {
        Click,
        Drag,
        BeginDrag,
        EndDrag,
        PointerEnter,
        PointerExit,
        Drop,
    }
    public enum GameplayTag
    {
        Player_Action_Jump,
        Player_Action_Attack,
        Player_Action_Parrying,
        Player_Action_Wave,
        Player_Action_UseItem,
        Player_Action_Interaction,
        Player_Action_Dash,
        Player_Action_AirAttack,
        Player_Action_DownJump,
        Player_Action_Block,
        Player_Action_Hurt,

        Player_State_Invincible,
        Player_State_Hurt,

        Player_Action_Charge,
        Player_Action_StrongAttack,
        Player_Action_Attack2,
        Player_Action_ParryingAttack,

        MaxCount
    }
    public enum GameplayAbility
    {
        GA_Jump,
        GA_Dash,
        GA_Attack,
        GA_WaveDetect,
        GA_AirAttack,
        GA_DownJump,
        GA_Block,
        GA_Parrying,
        GA_Hurt,
        GA_Invincible,
        GA_Charge,
        GA_StrongAttack,
        GA_Attack2,
        GA_ParryingAttack,

        None = 1000
    }
    public enum CameraMode
    {
        None,
        FollowTarget,
    }
    public enum Languages
    {
        Kor,
        Eng,
    }
    public enum DashType
    {
        DefaultDash,
        AttackDash,
    }
    
    public enum ModelType
    {
        Unknown,
        InteractionText,

    }
    public enum TutorialType
    {
        Move,
        Attack,
        Parrying,
        Jump,
        Dash,
        Item,
        StrongAttack,
        Fight,
    }
    public enum ConversationType
    {
        Prologe,
        SpawnMonsterTest,
    }
    public enum Direction
    {
        Up,
        RightUp,
        Right,
        RightDown,
        Down,
        LeftDown,
        Left,
        LeftUp,
    }

    public enum EnemyState
    {
        Default,
        Groggy,
        Dead,
        Hit,
        Attack,
    }
}
