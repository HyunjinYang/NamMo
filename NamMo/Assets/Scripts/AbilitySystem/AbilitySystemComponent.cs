using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    HashSet<Define.GameplayTag> _tagContainer = new HashSet<Define.GameplayTag>();
    Dictionary<Define.GameplayTag, GameAbility> _abilities;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GiveAbility(Define.GameplayTag tag)
    {
        if (!_tagContainer.Contains(tag))
        {
            Debug.Log("Already Exsist Tag");
            return;
        }
        GameAbility ability = CreateAbility(tag);
        _abilities.Add(tag, ability);
    }
    public void RemoveAbility(Define.GameplayTag tag)
    {
        if (_tagContainer.Contains(tag) == false)
        {
            Debug.Log("Not Exsist Tag");
            return;
        }
        _tagContainer.Remove(tag);
    }
    public bool IsExsistTag(Define.GameplayTag tag)
    {
        if (_tagContainer.Contains(tag)) return true;
        return false;
    }
    public void AddTag(Define.GameplayTag tag)
    {
        _tagContainer.Add(tag);
    }
    public void RemoveTag(Define.GameplayTag tag)
    {
        _tagContainer.Remove(tag);
    }
    private GameAbility CreateAbility(Define.GameplayTag tag)
    {
        GameAbility ability = null;
        switch (tag)
        {
            case Define.GameplayTag.Player_Action_Jump:
                ability = new GameAbility(this);
                break;
        }
        return ability;
    }
}
