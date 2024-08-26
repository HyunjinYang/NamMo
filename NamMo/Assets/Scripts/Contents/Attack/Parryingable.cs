using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParryingable
{
    void Parried(GameObject attacker, GameObject target = null);
}
