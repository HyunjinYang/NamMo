using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{

    private Dictionary<Define.GameplayTag, int> _tagContainer = new Dictionary<Define.GameplayTag, int>();
    private Dictionary<Define.GameplayAbility, GameAbility> _abilities = new Dictionary<Define.GameplayAbility, GameAbility>();
    //private List<Define.GameplayAbility> _reservedAbilities = new List<Define.GameplayAbility>();
    //private List<Define.GameplayAbility> _reservedCancelAbilities = new List<Define.GameplayAbility>();
    public PlayerController GetPlayerController()
    {
        return gameObject.GetComponent<PlayerController>();
    }
    //[SerializeField]
    //private int[] tagTest = new int[(int)Define.GameplayTag.MaxCount];
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
    public void UnlockAbility(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if (_abilities.TryGetValue(tag, out ga))
        {
            ga.CanUse = true;
        }
        else
        {
            Debug.Log("Not Exsist Ability");
        }
    }
    public void RemoveAbility(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        if (_abilities.TryGetValue(tag, out ga))
        {
            Destroy(ga.gameObject);
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
    //public void ReserveAbilityByTag(Define.GameplayAbility tag)
    //{
    //    if (_reservedAbilities.Contains(tag)) return;
    //    _reservedAbilities.Add(tag);
    //}
    //public void ReserveCancelAbilityByTag(Define.GameplayAbility tag)
    //{
    //    if (_reservedAbilities.Contains(tag) == false) return;
    //    if (_reservedCancelAbilities.Contains(tag)) return;
    //    _reservedCancelAbilities.Add(tag);
    //}
    //public void FlushReservedAbility()
    //{
    //    foreach(Define.GameplayAbility ability in _reservedAbilities)
    //    {
    //        TryActivateAbilityByTag(ability);
    //    }
    //    foreach (Define.GameplayAbility ability in _reservedCancelAbilities)
    //    {
    //        TryCancelAbilityByTag(ability);
    //    }
    //    _reservedAbilities.Clear();
    //    _reservedCancelAbilities.Clear();
    //}
    public void TryCancelAbilityByTag(Define.GameplayAbility tag, Define.GameplayAbility canceler = Define.GameplayAbility.None)
    {
        GameAbility ga = null;
        if (_abilities.TryGetValue(tag, out ga))
        {
            if (ga.IsActivated) ga.TryCancelAbility(canceler);
            else
            {
                //Debug.Log("Not Activated Ability");
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
            //tagTest[(int)tag] = cnt + 1;
        }
        else
        {
            _tagContainer.Add(tag, 1);
            //tagTest[(int)tag] = 1;
        }
    }
    public void RemoveTag(Define.GameplayTag tag)
    {
        int cnt = 0;
        if (_tagContainer.TryGetValue(tag, out cnt))
        {
            if (cnt == 1)
            {
                _tagContainer.Remove(tag);
                //tagTest[(int)tag] = 0;
            }
            else 
            { 
                _tagContainer[tag] = cnt - 1;
                //tagTest[(int)tag] = cnt - 1;
            }
        }
        else
        {
            Debug.Log("Not Exsist Tag");
        }
    }
    public GameAbility GetAbility(Define.GameplayAbility tag)
    {
        GameAbility ga = null;
        _abilities.TryGetValue(tag, out ga);
        return ga;
    }
    public List<Define.GameplayAbility> GetOwnedAbilities()
    {
        List<Define.GameplayAbility> abilities = new List<Define.GameplayAbility>();
        foreach(Define.GameplayAbility ability in _abilities.Keys)
        {
            if (GetAbility(ability).CanUse)
                abilities.Add(ability);
        }
        return abilities;
    }
    public void Clear()
    {
        foreach(Define.GameplayAbility abilityTag in _abilities.Keys)
        {
            TryCancelAbilityByTag(abilityTag);
        }
        //_reservedAbilities.Clear();
        //_reservedCancelAbilities.Clear();
    }
    private GameAbility CreateAbility(Define.GameplayAbility tag)
    {
        GameObject go = Managers.Resource.Instantiate("GameAbility/" + Enum.GetName(typeof(Define.GameplayAbility), tag));
        GameAbility ga = go.GetComponent<GameAbility>();
        go.transform.SetParent(transform);
        //go.layer = LayerMask.GetMask("Player");
        ga.AbilityTag = tag;
        ga.SetASC(this);
        return ga;
    }
}
