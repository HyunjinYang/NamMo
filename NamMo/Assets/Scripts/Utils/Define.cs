using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
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
    }
    public enum GameplayAbility
    {
        GA_Jump,
        GA_Dash,
    }
    public enum CameraMode
    {
        None,
        FollowTarget,
    }
}
