using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GA_Block : GameAbility
{
    [SerializeField] private float _perfectParryingTime;
    [SerializeField] private float _blockTime;
    private bool _isPerfectParryingTiming = false;
    private bool _reserveNextCombo = false;

    public Action<int> OnBlockComboChanged;

    private Coroutine _cacluateParryingTimingCoroutine = null;
    private Coroutine _blockCoroutine = null;

    private bool _reserveParrying = false;
    private Define.Direction _blockDirection = Define.Direction.Right;
    public bool ReserveParrying { get { return _reserveParrying; } }
    protected override void Init()
    {
        base.Init();
        _asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
    }
    protected override void ActivateAbility()
    {
        if (_overlapCnt == 0 || _reserveNextCombo)
        {
            base.ActivateAbility();
            _cacluateParryingTimingCoroutine = StartCoroutine(CoChangeParryingTypeByTimeFlow());
            _blockCoroutine = StartCoroutine(CoBlock());
        }
        else
        {
            _reserveNextCombo = true;
        }
    }
    public override bool CanActivateAbility()
    {
        if (base.CanActivateAbility() == false) return false;
        if (_reserveNextCombo) return false;
        return true;
    }
    protected override void CancelAbility()
    {
        if (_reserveNextCombo)
        {
            _overlapCnt++;
            _reserveNextCombo = false;
        }
        EndAbility();
    }
    protected override void EndAbility()
    {
        if (_cacluateParryingTimingCoroutine != null)
        {
            StopCoroutine(_cacluateParryingTimingCoroutine);
            _cacluateParryingTimingCoroutine = null;
        }
        if (_blockCoroutine != null)
        {
            StopCoroutine(_blockCoroutine);
            _blockCoroutine = null;
        }
        _isPerfectParryingTiming = false;

        if (_reserveNextCombo)
        {
            ActivateAbility();
        }
        else
        {
            base.EndAbility();
            _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
            //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered -= HandleTriggeredObject;
            _asc.GetPlayerController().GetBlockArea().DeActiveBlockArea();

            if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke(_overlapCnt);
        }
    }
    public void SetBlockDirection(Vector2 dir)
    {
        if (dir.x > 0.5f)
        {
            if (dir.y > 0.5f)
            {
                _blockDirection = Define.Direction.RightUp;
            }
            else if (dir.y < -0.5f)
            {
                _blockDirection = Define.Direction.RightDown;
            }
            else
            {
                _blockDirection = Define.Direction.Right;
            }
        }
        else if (dir.x < -0.5f)
        {
            if (dir.y > 0.5f)
            {
                _blockDirection = Define.Direction.LeftUp;
            }
            else if (dir.y < -0.5f)
            {
                _blockDirection = Define.Direction.LeftDown;
            }
            else
            {
                _blockDirection= Define.Direction.Left;
            }
        }
        else
        {
            if (dir.y > 0.5f)
            {
                _blockDirection = Define.Direction.Up;
            }
            else if (dir.y < -0.5f)
            {
                _blockDirection = Define.Direction.Down;
            }
            else
            {
                if (_asc.GetPlayerController().GetPlayerMovement().IsFacingRight)
                {
                    _blockDirection = Define.Direction.Right;
                }
                else
                {
                    _blockDirection = Define.Direction.Left;
                }
            }
        }
    }
    private int _attackStrength = 1;
    private void HandleTriggeredObject(GameObject go)
    {
        if (_isPerfectParryingTiming)
        {
            if (true/*_asc.IsExsistTag(Define.GameplayTag.Player_State_Hurt) == false*/)
            {
                IParryingable parryingable = go.GetComponent<IParryingable>();
                if (parryingable != null)
                {
                    _attackStrength = Math.Max(_attackStrength, go.GetComponent<BaseAttack>().AttackStrength);
                    
                    if (_reserveParrying == false)
                    {
                        _reserveParrying = true;
                        StartCoroutine(CoCancelAbility(go));
                    }
                    parryingable.Parried(_asc.GetPlayerController().gameObject, null);
                }
                else
                {
                    Debug.Log(go.name);
                }
                
            }
        }
    }
    IEnumerator CoBlock()
    {
        _reserveNextCombo = false;
        _asc.gameObject.GetComponent<PlayerMovement>().CanMove = false;
        //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
        _asc.GetPlayerController().GetBlockArea().SetDirection(_blockDirection);
        _asc.GetPlayerController().GetBlockArea().ActiveBlockArea();
        Debug.Log($"Block Combo : {(_overlapCnt - 1) % 3 + 1}");
        if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke((_overlapCnt - 1) % 3 + 1);

        yield return new WaitForSeconds(_blockTime);

        _blockCoroutine = null;
        EndAbility();
    }
    IEnumerator CoChangeParryingTypeByTimeFlow()
    {
        _isPerfectParryingTiming = true;
        yield return new WaitForSeconds(_perfectParryingTime);
        _isPerfectParryingTiming = false;
        _cacluateParryingTimingCoroutine = null;
    }
    IEnumerator CoCancelAbility(GameObject go)
    {
        float dir = 1;
        if (go.transform.position.x > _asc.GetPlayerController().transform.position.x) dir = -1;

        yield return new WaitForEndOfFrame();
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Block);
        RefreshCoolTime();

        float knockbackPower = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.GA_Parrying].reactValues[_attackStrength].knockbackPower;
        float blockCancelTime = Managers.Data.EnemyAttackReactDict[Define.GameplayAbility.GA_Parrying].reactValues[_attackStrength].bindTime;
        _asc.GetAbility(Define.GameplayAbility.GA_Parrying).BlockCancelTime = blockCancelTime;
        Managers.Scene.CurrentScene.Player.GetPlayerMovement().AddForce(new Vector2(dir, 0), knockbackPower);
        
        _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Parrying);
        _reserveParrying = false;
        _attackStrength = 1;
    }
}
