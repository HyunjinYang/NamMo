using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Enemy;
using UnityEngine;

public abstract partial class BaseAttack : MonoBehaviour
{
    public enum AttackerType
    {
        Player,
        Enemy,
        Others,
    }
    protected GameObject _attacker;
    protected float _damage;

    protected AttackerType _attackerType;

    public Action<GameObject> OnHitted;
    public GameObject Attakcer { get { return _attacker; } }
    public float Damage { get { return _damage; } }
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
    public virtual void SetAttackInfo(GameObject attacker, float damage, float speed = 0, GameObject target = null)
    {
        _attacker = attacker;
        _damage = damage;

        if (attacker.GetComponent<PlayerController>()) _attackerType = AttackerType.Player;
        else if (attacker.GetComponent<Enemy.Enemy>()) _attackerType = AttackerType.Enemy;
        else _attackerType = AttackerType.Others;
    }

    // protected
    protected virtual void Init() { }
    protected virtual void UpdateAttack() { }
    protected virtual void FixedUpdateAttack() { }
    protected void TryHit(GameObject target)
    {
        if(_attackerType == AttackerType.Player)
        {
            TryHitEnemy(target);
            TryHitObject(target);
        }
        else if(_attackerType == AttackerType.Enemy)
        {
            TryHitPlayer(target);
        }
        else if(_attackerType == AttackerType.Others)
        {
            TryHitPlayer(target);
        }
        if (OnHitted != null) OnHitted.Invoke(target);
    }
    private void TryHitPlayer(GameObject target)
    {
        if (target.GetComponent<PlayerController>())
        {
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc.GetASC().IsExsistTag(Define.GameplayTag.Player_State_Invincible)) return;
            pc.GetPlayerCombatComponent().GetDamaged(_damage, transform.position);
        }
    }
    private void TryHitEnemy(GameObject target)
    {
        if (target.GetComponent<Enemy.Enemy>())
        {
            Enemy.Enemy enemy = target.GetComponent<Enemy.Enemy>();
            enemy.Hit((int)_damage);
        }
        // tmp
        else if (target.GetComponent<DummyEnemy>())
        {
            target.GetComponent<DummyEnemy>().Damaged(_damage);
        }
    }
    private void TryHitObject(GameObject target)
    {
        if (target.GetComponent<BreakWall>())
        {
            target.GetComponent<BreakWall>().Damaged();
        }
        if (target.GetComponent<TutorialNPC>())
        {
            target.GetComponent<TutorialNPC>().Damaged(_attacker.transform.position.x);
        }
    }
    
}