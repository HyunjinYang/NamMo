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
    [SerializeField] private float _intersactionWaveInputTime;
    [Header("CheatMode")]
    [SerializeField] private bool _cheatMode = false;
    [SerializeField] private bool _bossCheatMode = false;

    public Action<float> OnMoveInputChanged;
    public Action OnAttackInputPerformed;
    public Action OnInteractionInputPerformed;

    private PlayerMovement _pm;
    private PlayerStat _ps;
    private PlayerCombatComponent _pcc;
    private PlayerSound _playerSound;
    
    private bool _pushDown = false;
    private bool _waveInputDetect = false;
    private float _waveInputPushTime = 0;
    private void Awake()
    {
        Managers.Scene.CurrentScene.SetPlayerController(this);

        _pm = gameObject.GetComponent<PlayerMovement>();
        _ps = gameObject.GetComponent<PlayerStat>();
        _pcc = gameObject.GetComponent<PlayerCombatComponent>();
        _playerSound = gameObject.GetComponent<PlayerSound>();
        _ps.SetPlayerController(this);
        _pcc.SetPlayerController(this);
        _playerSound.SetPlayerController(this);
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

        if (_bossCheatMode) _ps.OnDead += BossDeadTest;
        Camera.main.GetComponent<CameraController>().SetTargetInfo(gameObject);
    }
    private void Update()
    {
        if (_waveInputDetect)
        {
            _waveInputPushTime += Time.deltaTime;
        }
    }
    public AbilitySystemComponent GetASC() { return _asc; }
    public PlayerMovement GetPlayerMovement() { return _pm; }
    public PlayerStat GetPlayerStat() { return _ps; }
    public PlayerCombatComponent GetPlayerCombatComponent() { return _pcc; }
    public PlayerSound GetPlayerSound() { return _playerSound; }
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
        Managers.Data.EnemyData = GameData.Load<EnemyData>();
        Respawn();
    }

    private void BossDeadTest()
    {
        Time.timeScale = 0;
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
    private bool _blockInput = false;
    public bool BlockInput
    {
        get { return _blockInput; }
        set
        {
            _blockInput = value;
            OnMoveInputChanged.Invoke(0);
        }
    }
    // �̵�
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        if (context.started)
        {
        }
        else if (context.performed)
        {
            if (BlockInput) return;
            OnMoveInputChanged.Invoke(value.x);
        }
        else if (context.canceled)
        {
            OnMoveInputChanged.Invoke(value.y);
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
            if (BlockInput) return;
            if (_pushDown)
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_DownJump);
            }
            else
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Jump);
            }

        }
        else if (context.canceled)
        {
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Jump);
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_DownJump);
        }
    }
    // �ϴ�
    public void HandleDownInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (BlockInput) return;
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
        if (BlockInput) return;
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
    public void HandleChargeAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (BlockInput) return;
            if (_asc.GetAbility(Define.GameplayAbility.GA_Charge).CanActivateAbility() == false) return;
            _waveInputPushTime = 0;
            _waveInputDetect = true;
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Charge);
        }
        else if (context.canceled)
        {
            if (_asc.IsExsistTag(Define.GameplayTag.Player_Action_Charge) == false) return;
            _asc.TryCancelAbilityByTag(Define.GameplayAbility.GA_Charge);
            if (_waveInputPushTime < _intersactionWaveInputTime)
            {
                //_asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_WaveDetect);
            }
            else
            {
                _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_StrongAttack);
            }
            _waveInputPushTime = 0;
            _waveInputDetect = false;
        }
    }
    // �ĵ�Ž��
    public void HandleWaveInput(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_WaveDetect);
        }
    }
    // �и�
    public void HandleParryingInput(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Block);
        }
    }
    // �뽬
    public void HandleDashInput(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            _asc.TryActivateAbilityByTag(Define.GameplayAbility.GA_Dash);
        }
    }
    // ��ȣ�ۿ�
    public void HandleInteractionInput(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            if (OnInteractionInputPerformed != null) OnInteractionInputPerformed.Invoke();
        }
    }
    // ������1
    public void HandleUseItem1Input(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            Debug.Log("HandleUseItem1Input");
        }
    }
    // ������2
    public void HandleUseItem2Input(InputAction.CallbackContext context)
    {
        if (BlockInput) return;
        if (context.performed)
        {
            Debug.Log("HandleUseItem2Input");
        }
    }
}