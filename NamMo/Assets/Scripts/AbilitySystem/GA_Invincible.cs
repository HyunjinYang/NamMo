using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Invincible : GameAbility
{
    [SerializeField] private float _invincibleTime;
    protected override void ActivateAbility()
    {
        base.ActivateAbility();

        StartCoroutine(CoInvincible());
        _asc.gameObject.GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask("EnemyAttack");
    }
    protected override void EndAbility()
    {
        base.EndAbility();
        _asc.gameObject.GetComponent<Collider2D>().excludeLayers = ~-1;
    }
    IEnumerator CoInvincible()
    {
        yield return new WaitForSeconds(_invincibleTime);
        EndAbility();
    }
}
