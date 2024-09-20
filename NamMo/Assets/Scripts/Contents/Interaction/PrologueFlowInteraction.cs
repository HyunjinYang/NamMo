using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueFlowInteraction : BaseInteractable
{
    public Action OnInteraction;
    protected override void HandleTriggerEnterEvent()
    {
        if (OnInteraction != null) OnInteraction.Invoke();
    }
}
