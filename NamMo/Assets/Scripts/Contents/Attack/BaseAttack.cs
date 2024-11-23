using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using BehaviorTree_Enemy;
using Enemy;
using UnityEngine;

public abstract partial class BaseAttack : MonoBehaviour
{
    public enum AttackerType
    {
        Player,
        Enemy,
        TestEnemy,
        Others,
    }
    protected GameObject _attacker;
    protected float _damage;
    protected int _attackStrength = 1;

    protected AttackerType _attackerType;

    public Action<GameObject> OnHitted;
    public GameObject Attacker { get { return _attacker; } }
    public float Damage { get { return _damage; } }
    public int AttackStrength { get { return _attackStrength; } }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        UpdateAttack();
    }
    private void FixedUpdate()
    {
        FixedUpdateAttack();
    }
    
    private void OnDestroy()
    {
        OnHitted = null;
    }
    // public
    public virtual void SetAttackInfo(GameObject attacker, float damage, int attackStrength = 1, float speed = 0, GameObject target = null)
    {
        _attacker = attacker;
        _damage = damage;
        _attackStrength = attackStrength;
        if (attacker.GetComponent<PlayerController>()) _attackerType = AttackerType.Player;
        else if (attacker.GetComponent<Enemy.Enemy>()) _attackerType = AttackerType.Enemy;
        else if (attacker.GetComponent<TestEnemy>()) _attackerType = AttackerType.TestEnemy;
        else _attackerType = AttackerType.Others;
    }
    // protected
    protected virtual void Init() { }
    protected virtual void UpdateAttack() { }
    protected virtual void FixedUpdateAttack() { }
    protected void TryHit(GameObject target)
    {
        bool _hitted = false;
        if(_attackerType == AttackerType.Player)
        {
            _hitted |= TryHitEnemy(target);
            _hitted |= TryHitObject(target);
        }
        else if(_attackerType == AttackerType.Enemy)
        {
            _hitted |= TryHitPlayer(target);
        }
        else if (_attackerType == AttackerType.TestEnemy)
        {
            _hitted |= TryHitPlayer(target);
        }
        else if(_attackerType == AttackerType.Others)
        {
            _hitted |= TryHitPlayer(target);
        }
        if (_hitted)
        {
            if (OnHitted != null) OnHitted.Invoke(target);
        }
    }
    private bool TryHitPlayer(GameObject target)
    {
        if (target.GetComponent<PlayerController>())
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc.GetASC().IsExsistTag(Define.GameplayTag.Player_State_Invincible)) return false;
            pc.GetPlayerCombatComponent().GetDamaged(_damage, transform.position, _attackStrength);
            return true;
        }
        return false;
    }
    private bool TryHitEnemy(GameObject target)
    {
        bool res = false;
        if (target.GetComponent<Enemy.Enemy>())
        {
            Enemy.Enemy enemy = target.GetComponent<Enemy.Enemy>();
            enemy.Hit((int)_damage);
            res = true;
        }
        else if (target.GetComponent<TestEnemy>())
        {
            TestEnemy enemy = target.GetComponent<TestEnemy>();
            enemy.Hit((int)_damage);
            res = true;
        }
        // tmp
        else if (target.GetComponent<DummyEnemy>())
        {
            target.GetComponent<DummyEnemy>().Damaged(_damage);
            res = true;
        }
        return res;
    }
    private bool TryHitObject(GameObject target)
    {
        bool res = false;
        if (target.GetComponent<BreakWall>())
        {
            target.GetComponent<BreakWall>().Damaged();
            res = true;
        }
        if (target.GetComponent<TutorialNPC>())
        {
            target.GetComponent<TutorialNPC>().Damaged(_attacker.transform.position.x);
            res = true;
        }
        return res;
    }
    
}