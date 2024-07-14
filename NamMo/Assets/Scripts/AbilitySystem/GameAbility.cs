using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAbility
{
    [SerializeField] protected List<Define.GameplayTag> _tagsToAdd;
    [SerializeField] protected List<Define.GameplayTag> _blockTags;
    private AbilitySystemComponent _asc;
    public GameAbility(AbilitySystemComponent asc)
    {
        _asc = asc;
    }
    protected virtual void TryActivateAbility()
    {
        Debug.Log("TryActivateAbility");
        if (CanActivateAbility() == false) return;
    }
    protected virtual bool CanActivateAbility()
    {
        return true;
    }
}
