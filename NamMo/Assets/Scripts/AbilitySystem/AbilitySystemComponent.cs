using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    Dictionary<Define.GameplayTag, int> _tagContainer = new Dictionary<Define.GameplayTag, int>();
    Dictionary<Define.GameplayAbility, GameAbility> _abilities = new Dictionary<Define.GameplayAbility, GameAbility>();
    public void GiveAbility(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if(_abilities.TryGetValue(tag, out ga))
        {
            Debug.Log("Already Exsist Ability");
            return;
        }
        ga = CreateAbility(tag);
        _abilities.Add(tag, ga);
    }
    public void RemoveAbility(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if (_abilities.TryGetValue(tag, out ga))
        {
            _abilities.Remove(tag);
        }
        else
        {
            Debug.Log("Not Exsist Ability");
        }
    }
    public void TryActivateAbilityByTag(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if(_abilities.TryGetValue(tag, out ga))
        {
            ga.TryActivateAbility();
        }
        else
        {
            Debug.Log("Not Exsist Ability");
        }
    }
    public void TryCancelAbilityByTag(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if (_abilities.TryGetValue(tag, out ga))
        {
            if (ga.IsActivated) ga.CancelAbility();
            else
            {
                Debug.Log("Not Activated Ability");
            }
        }
        else
        {
            Debug.Log("Not Exsist Ability");
        }
    }
    public bool IsExsistTag(Define.GameplayTag tag)
    {
        int cnt = 0;
        if(_tagContainer.TryGetValue(tag,out cnt))
        {
            return true;
        }
        return false;
    }
    public void AddTag(Define.GameplayTag tag)
    {
        int cnt = 0;
        if (_tagContainer.TryGetValue(tag, out cnt))
        {
            _tagContainer[tag] = cnt + 1;
        }
        else
        {
            _tagContainer.Add(tag, 1);
        }
    }
    public void RemoveTag(Define.GameplayTag tag)
    {
        int cnt = 0;
        if (_tagContainer.TryGetValue(tag, out cnt))
        {
            if (cnt == 1) _tagContainer.Remove(tag);
            else _tagContainer[tag] = cnt - 1;
        }
        else
        {
            Debug.Log("Not Exsist Tag");
        }
    }
    private GameAbility CreateAbility(Define.GameplayAbility tag)
    {
        GameObject go = Managers.Resource.Instantiate("GameAbility/" + Enum.GetName(typeof(Define.GameplayAbility), tag));
        GameAbility ga = go.GetComponent<GameAbility>();
        go.transform.SetParent(transform);
        ga.SetASC(this);
        return ga;
    }
}
