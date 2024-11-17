using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GA_Block : GameAbility
{
    [SerializeField] private float _perfectParryingTime;
    [SerializeField] private float _blockTime;
    [SerializeField] private GameObject _waveDetectEffectPrefab;
    private bool _isPerfectParryingTiming = false;
    private bool _reserveNextCombo = false;

    public Action<int> OnBlockComboChanged;

    private Coroutine _cacluateParryingTimingCoroutine = null;
    private Coroutine _blockCoroutine = null;

    private bool _reserveParrying = false;
    private Define.Direction _blockDirection = Define.Direction.Right;
    private int _canMoveCnt = 0;
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
            for (int i = 0; i < _canMoveCnt; i++)
            {
                _asc.gameObject.GetComponent<PlayerMovement>().CanMove = true;
            }
            _canMoveCnt = 0;
            //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered -= HandleTriggeredObject;
            _asc.GetPlayerController().GetBlockArea().DeActiveBlockArea();

            if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke(_overlapCnt);
        }
    }
    public void SetBlockDirection(Vector2 dir)
    {
        /*
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
        }*/
        if (dir.y > 0.5f)
        {
            _blockDirection = Define.Direction.Up;
        }
        else
        {
            if (dir.x > 0.5f)
            {
                _blockDirection = Define.Direction.Right;
            }
            else if (dir.x < -0.5f)
            {
                _blockDirection = Define.Direction.Left;
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
    private List<BaseAttack> _parriedObjects = new List<BaseAttack>();
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
                        StartCoroutine(CoActivateParrying(go));
                    }
                    parryingable.Parried(_asc.GetPlayerController().gameObject, null);
                    if (go.GetComponent<BaseAttack>())
                    {
                        _parriedObjects.Add(go.GetComponent<BaseAttack>());
                    }
                    Vector2 point = _asc.GetPlayerController().GetBlockArea().GetComponent<Collider2D>().ClosestPoint(go.transform.position);
                    GameObject _blockEffect = Instantiate(_waveDetectEffectPrefab, point, Quaternion.identity);
                    _blockEffect.transform.localScale = Vector3.one * 0.1f;
                    _blockEffect.GetComponent<Renderer>().sortingOrder = 1;
                    _blockEffect.GetComponent<VFXController>().Play(0.25f, 0.75f);
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
        _canMoveCnt++;
        //_asc.GetPlayerController().GetBlockArea().OnBlockAreaTriggerEntered += HandleTriggeredObject;
        _asc.GetPlayerController().GetBlockArea().SetDirection(_blockDirection);
        _asc.GetPlayerController().GetBlockArea().ActiveBlockArea();
        Debug.Log($"Block Combo : {(_overlapCnt - 1) % 3 + 1}");
        if (OnBlockComboChanged != null) OnBlockComboChanged.Invoke((_overlapCnt - 1) % 3 + 1);

        yield return new WaitForSecondsRealtime(_blockTime);

        _blockCoroutine = null;
        EndAbility();
    }
    IEnumerator CoChangeParryingTypeByTimeFlow()
    {
        _isPerfectParryingTiming = true;
        yield return new WaitForSecondsRealtime(_perfectParryingTime);
        _isPerfectParryingTiming = false;
        _cacluateParryingTimingCoroutine = null;
    }
    IEnumerator CoActivateParrying(GameObject go)
    {
        float dir = 1;
        if (go.transform.position.x > _asc.GetPlayerController().transform.position.x) dir = -1;

        yield return new WaitForEndOfFrame();
        _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Block);
        RefreshCoolTime();

        GA_Parrying parryingAbility = _asc.GetAbility(Define.GameplayAbility.GA_Parrying) as GA_Parrying;
        if (parryingAbility)
        {
            parryingAbility.ParriedAttacks.Clear();
            foreach(BaseAttack parriedAttack in _parriedObjects)
            {
                parryingAbility.ParriedAttacks.Add(parriedAttack);
            }
            parryingAbility.SetParriedAttackInfo(dir, _attackStrength);
        }

        _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Parrying);
        _reserveParrying = false;
        _attackStrength = 0;

        _parriedObjects.Clear();
    }
}
