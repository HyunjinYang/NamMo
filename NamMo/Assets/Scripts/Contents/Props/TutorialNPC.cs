using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCPhase
{
    Idle,
    CloseAttack,
    RangeAttack,
}
public enum NPCState
{
    Idle,
    Damaged,
    Trace,
    CloseAttack,
    RangeAttack,
}
public class TutorialNPC : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CloseAttack _attackArea;

    [SerializeField] private float _speed;
    [SerializeField] private float _knockbackPower;
    [SerializeField] private bool _isFacingRight = false;

    [SerializeField] private float _damagedTime;
    [SerializeField] private float _closeAttackInterval;
    [SerializeField] private float _rangeAttackInterval;

    [SerializeField] private float _closeAttackDistance;
    [SerializeField] private float _rangeAttackDistance;

    private float _currentStateTime = 0f;

    private NPCState _npcState = NPCState.Idle;
    private PlayerController _player = null;

    private Coroutine _closeAttackCoroutine = null;
    private Coroutine _rangeAttackCoroutine = null;
    private float _attackerPosX;

    public NPCPhase CurrentPhase = NPCPhase.Idle;

    public Action OnDamaged;
    private void Start()
    {
        _player = Managers.Scene.CurrentScene.Player;
    }
    private void Update()
    {
        _currentStateTime += Time.deltaTime;
        switch (_npcState)
        {
            case NPCState.Idle:
                UpdateIdle();
                break;
            case NPCState.Damaged:
                UpdateDamaged();
                break;
            case NPCState.Trace:
                UpdateTrace();
                break;
            case NPCState.CloseAttack:
                UpdateCloseAttack();
                break;
            case NPCState.RangeAttack:
                UpdateRangeAttack();
                break;
        }
    }
    private void UpdateIdle()
    {
        LookPlayer();
        if (CurrentPhase == NPCPhase.Idle) return;
        if(CurrentPhase == NPCPhase.CloseAttack)
        {
            if (_currentStateTime > _closeAttackInterval)
            {
                ChangeState(NPCState.Trace);
            }
        }
        else if(CurrentPhase == NPCPhase.RangeAttack)
        {
            if (_currentStateTime > _rangeAttackInterval)
            {
                ChangeState(NPCState.Trace);
            }
        }
    }
    private void UpdateDamaged()
    {
        int dir;
        if (_attackerPosX > transform.position.x) dir = -1;
        else dir = 1;

        transform.position = new Vector3(transform.position.x + dir * _knockbackPower * Time.deltaTime, transform.position.y, transform.position.z);

        if (_currentStateTime > _damagedTime)
        {
            ChangeState(NPCState.Idle);
        }
    }
    private void UpdateTrace()
    {
        LookPlayer();
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        int dir;
        if (_player.transform.position.x > transform.position.x) dir = 1;
        else dir = -1;

        if (CurrentPhase == NPCPhase.CloseAttack)
        {
            if(distance <= _closeAttackDistance)
            {
                ChangeState(NPCState.CloseAttack);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + dir * _speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
        else if (CurrentPhase == NPCPhase.RangeAttack)
        {
            if (distance <= _rangeAttackDistance)
            {
                ChangeState(NPCState.RangeAttack);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + dir * _speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }
    private void UpdateCloseAttack()
    {

    }
    private void UpdateRangeAttack()
    {

    }
    private void LookPlayer()
    {
        if (_player.transform.position.x > transform.position.x)
        {
            if (_isFacingRight == false)
            {
                Flip();
            }
        }
        else
        {
            if (_isFacingRight)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void ChangeState(NPCState state)
    {
        _currentStateTime = 0;
        _npcState = state;
        switch (_npcState)
        {
            case NPCState.Idle:
                _animator.Play("Idle");
                break;
            case NPCState.Damaged:
                if (_closeAttackCoroutine != null)
                {
                    StopCoroutine(_closeAttackCoroutine);
                    _attackArea.DeActiveAttackArea();
                }
                if (_rangeAttackCoroutine != null)
                {
                    StopCoroutine(_rangeAttackCoroutine);
                }
                _animator.Play("Hurt", -1, 0);
                break;
            case NPCState.Trace:
                _animator.Play("Walk");
                break;
            case NPCState.CloseAttack:
                _animator.Play("CloseAttack");
                _closeAttackCoroutine = StartCoroutine(CoCloseAttack());
                break;
            case NPCState.RangeAttack:
                _animator.Play("RangeAttack");
                // tmp
                _closeAttackCoroutine = StartCoroutine(CoCloseAttack());

                //_rangeAttackCoroutine = StartCoroutine(CoRangeAttack());
                break;
        }
    }
    public void Damaged(float attackerPosX)
    {
        if (OnDamaged != null)
        {
            OnDamaged.Invoke();
        }
        _attackerPosX = attackerPosX;
        ChangeState(NPCState.Damaged);
    }
    IEnumerator CoCloseAttack()
    {
        yield return new WaitForSeconds(0.4f);
        _attackArea.SetAttackInfo(gameObject, 0);
        _attackArea.ActiveAttackArea();
        yield return new WaitForFixedUpdate();
        _attackArea.DeActiveAttackArea();
        yield return new WaitForSeconds(0.4f);
        ChangeState(NPCState.Idle);
        _closeAttackCoroutine = null;
    }
    IEnumerator CoRangeAttack()
    {
        yield return new WaitForSeconds(0.15f);
        // TODO : °Ë±â
        yield return new WaitForSeconds(0.25f);
        ChangeState(NPCState.Idle);
        _rangeAttackCoroutine = null;
    }
}
