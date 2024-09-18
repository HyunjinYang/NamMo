using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : CloseAttack, IParryingable
{
    public void Parried(GameObject attacker, GameObject target = null)
    {
        Debug.Log("Parried");
    }
}
