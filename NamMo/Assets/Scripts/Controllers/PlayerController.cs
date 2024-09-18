using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilitySystemComponent _asc;
    [SerializeField] private BlockArea _blockArea;
    [SerializeField] private CloseAttack _attackArea;
    [SerializeField] private List<Define.GameplayAbility> _abilities;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private WaveTrigger _waveTrigger;
    [Header("CheatMode")]
    [SerializeField] private bool _cheatMode = false;

    public Action<float> OnMoveInputChanged;
    public Action OnAttackInputPerformed;
    public Action OnInteractionInputPerformed;

    private PlayerMovement _pm;
    private PlayerStat _ps;
    private PlayerCombatComponent _pcc;
    
    private bool _pushDown = false;
    private void Awake()
    {
        Managers.Scene.CurrentScene.SetPlayerController(this);

        _pm = gameObject.GetComponent<PlayerMovement>();
        _ps = gameObject.GetComponent<PlayerStat>();
        _pcc = gameObject.GetComponent<PlayerCombatComponent>();
        _ps.SetPlayerController(this);
        _pcc.SetPlayerController(this);
        if (_asc == null) _asc = GetComponent<AbilitySystemComponent>();
        foreach(var ability in _abilities)
        {
            _asc.GiveAbility(ability);
            if (_cheatMode) _asc.GetAbility(ability).CanUse = true;
        }
        if (_cheatMode == false)
        {
            _ps.OnDead += Dead;
        }
        Camera.main.GetComponent<CameraController>().SetTargetInfo(gameObject);
    }
    public AbilitySystemComponent GetASC() { return _asc; }
    public PlayerMovement GetPlayerMovement() { return _pm; }
    public PlayerStat GetPlayerStat() { return _ps; }
    public PlayerCombatComponent GetPlayerCombatComponent() { return _pcc; }
    public BlockArea GetBlockArea() { return _blockArea; }
    public CloseAttack GetAttackArea() { return _attackArea; }
    public GameObject GetPlayerSprite() { return _playerSprite; }
    public WaveTrigger GetWaveTrigger() { return _waveTrigger; }
    public void SetPlayerInfoByPlayerData()
    {
        PlayerData playerData = Managers.Data.PlayerData;

        _ps.SetHealthInfo(playerData.Hp, playerData.MaxHp);
        foreach (Define.GameplayAbility ability in playerData.Abilities)
        {
            _asc.GetAbility(ability).CanUse = true;
        }
        GA_WaveDetect waveDetectAbility = _asc.GetAbility(Define.GameplayAbility.GA_WaveDetect) as GA_WaveDetect;
        if (waveDetectAbility)
        {
            waveDetectAbility.RemainUseCnt = playerData.WaveDetectCnt;
        }
    }
    private void Dead()
    {
        // 리스폰 전 사전 작업 ex) UI 띄우기 등
        Managers.Data.PlayerData = GameData.Load<PlayerData>();
        Respawn();
    }
    private void Respawn()
    {
        // 마지막 저장 정보를 가져와서 리스폰시킨다.
        // 체력, 파동횟수, 보유 ability
        PlayerData.Respawn = true;

        PlayerData playerData = Managers.Data.PlayerData;
        Managers.Scene.LoadScene(playerData.LocateScene);
    }
}
// Handle Input
public partial class PlayerController : MonoBehaviour
{
    // �̵�
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        if (context.started)
        {
        }
        else if (context.performed)
        {
            OnMoveInputChanged.Invoke(value);
        }
        else if (context.canceled)
        {
            OnMoveInputChanged.Invoke(value);
        }
    }
    // ����
    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
        }
        else if (context.performed)
        {
            if (_pushDown)
            {
                if (_asc.IsExsistTag(Define.GameplayTag.Player_Action_Attack))
                {
                    _asc.ReserveAbilityByTag(Define.GameplayAbility.GA_DownJump);
                }
                else
                {
                    _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_DownJump);
                }
            }
            else
            {
                if (_asc.IsExsistTag(Define.GameplayTag.Player_Action_Attack))
                {
                    _asc.ReserveAbilityByTag(Define.GameplayAbility.GA_Jump);
                }
                else
                {
                    _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Jump);
                }
            }

        }
        else if (context.canceled)
        {
            _asc.ReserveCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
        }
    }
    // �ϴ�
    public void HandleDownInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _pushDown = true;
        }
        else if (context.canceled)
        {
            _pushDown = false;
        }
    }
    // ����
    public void HandleAttackInput(InputAction.CallbackContext context)
    {
        if (_pm.IsJumping || _pm.IsFalling)
        {
            if (context.performed)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_AirAttack);
            }
        }
        else
        {
            if (context.performed)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Attack);
            }
        }
    }
    // �ĵ�Ž��
    public void HandleWaveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_WaveDetect);
        }
    }
    // �и�
    public void HandleParryingInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Block);
        }
    }
    // �뽬
    public void HandleDashInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_asc.IsExsistTag(Define.GameplayTag.Player_Action_Attack) || _asc.IsExsistTag(Define.GameplayTag.Player_Action_AirAttack))
            {
                _asc.ReserveAbilityByTag(Define.GameplayAbility.GA_Dash);
            }
            else
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Dash);
            }
        }
    }
    // ��ȣ�ۿ�
    public void HandleInteractionInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (OnInteractionInputPerformed != null) OnInteractionInputPerformed.Invoke();
        }
    }
    // ������1
    public void HandleUseItem1Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleUseItem1Input");
        }
    }
    // ������2
    public void HandleUseItem2Input(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("HandleUseItem2Input");
        }
    }
}