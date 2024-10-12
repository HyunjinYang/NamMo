using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : BaseInteractable
{
    protected override void HandleInteractionEvent()
    {
        base.HandleInteractionEvent();
        PlayerController player = Managers.Scene.CurrentScene.Player;
        player.GetPlayerStat().ApplyHeal(player.GetPlayerStat().MaxHp);

        //Managers.Data.PlayerData.Save();
        Managers.Data.SaveAllData();
    }
}
