using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameAbility : MonoBehaviour
{
    [SerializeField] protected bool _canOverlapAbility = false;
    // 발동될때 추가될 태그, 발동이 종료될때 삭제될 태그
    [SerializeField] protected List<Define.GameplayTag> _tagsToAdd;
    // 발동에 필요한 태그
    [SerializeField] protected List<Define.GameplayTag> _needTags;
    // 발동할 때 있으면 안되는 태그
    [SerializeField] protected List<Define.GameplayTag> _blockTags;
    // 발동될 때 취소할 능력
    [SerializeField] protected List<Define.GameplayAbility> _cancelAbilities;
    [SerializeField] protected float _coolTime;
    protected AbilitySystemComponent _asc;
    protected int _overlapCnt = 0;
    protected bool _blockCancelAbility = false;

    private bool _isActivated = false;
    private bool _isCoolTime = false;
    [SerializeField] public bool CanUse = false;
    public Action OnAbilityActivated;
    public Action OnAbilityCanceled;
    public Action OnAbilityEnded;
    public Action<float> OnCooltimeStart;
    public Action<float> OnCooltimeStart_Combo;
    public bool IsActivated { get { return _isActivated; } }
    public bool BlockCancelAbility { get { return _blockCancelAbility; } }
    public float BlockCancelTime = 0f;
    private void Start()
    {
        Init();
    }
    public void SetASC(AbilitySystemComponent asc)
    {
        _asc = asc;
    }
    public void TryActivateAbility()
    {
        if (CanActivateAbility() == false) return;
        foreach(Define.GameplayTag tag in _tagsToAdd)
        {
            _asc.AddTag(tag);
        }
        ActivateAbility();
        return;
    }
    public void TryCancelAbility()
    {
        if(CanCancelAbility() == false) return;
        CancelAbility();
    }
    protected virtual void Init()
    {

    }
    protected virtual void ActivateAbility()
    {
        _isActivated = true;
        _overlapCnt++;
        foreach(Define.GameplayAbility ga in _cancelAbilities)
        {
            _asc.TryCancelAbilityByTag(ga);
        }
        if(_coolTime > 0 && _canOverlapAbility == false)
        {
            _isCoolTime = true;
            StartCoroutine(CoCaculateCoolTime());
            if (OnCooltimeStart != null) OnCooltimeStart.Invoke(_coolTime);
        }
        if (OnAbilityActivated != null) OnAbilityActivated.Invoke();
    }
    public virtual bool CanActivateAbility()
    {
        if (CanUse == false) return false;
        if (_isCoolTime) return false;
        foreach(Define.GameplayTag tag in _needTags)
        {
            if (_asc.IsExsistTag(tag) == false) return false;
        }
        foreach(Define.GameplayTag tag in _blockTags)
        {
            if (_asc.IsExsistTag(tag)) return false;
        }
        foreach(Define.GameplayAbility abilityTag in _cancelAbilities)
        {
            GameAbility ability = _asc.GetAbility(abilityTag);
            if (ability)
            {
                if (ability.IsActivated && ability.BlockCancelAbility) return false;
            }
        }
        if (_canOverlapAbility == false)
        {
            if (_isActivated) return false;
        }
        return true;
    }
    public virtual bool CanCancelAbility()
    {
        return !_blockCancelAbility;
    }
    protected virtual void EndAbility()
    {
        for(int i=0;i<_overlapCnt;i++)
        {
            RemoveTags();
        }
        _isActivated = false;
        _overlapCnt = 0;
        if (_coolTime > 0 && _canOverlapAbility)
        {
            _isCoolTime = true;
            StartCoroutine(CoCaculateCoolTime());
            if (OnCooltimeStart_Combo != null) OnCooltimeStart_Combo.Invoke(_coolTime);
        }
        if (OnAbilityEnded != null) OnAbilityEnded.Invoke();
    }
    protected virtual void CancelAbility()
    {
        if (OnAbilityCanceled != null) OnAbilityCanceled.Invoke();
    }
    private void RemoveTags()
    {
        foreach (Define.GameplayTag tag in _tagsToAdd)
        {
            _asc.RemoveTag(tag);
        }
    }
    protected void RefreshCoolTime()
    {
        if(_coolTime > 0)
        {
            _coolTime = 0;
        }
    }
    private IEnumerator CoCaculateCoolTime()
    {
        yield return new WaitForSeconds(_coolTime);
        _isCoolTime = false;
    }
    protected IEnumerator CoBlockCancelAbility()
    {
        _blockCancelAbility = true;
        yield return new WaitForSeconds(BlockCancelTime);
        _blockCancelAbility = false;
        BlockCancelTime = 0f;
    }
}
