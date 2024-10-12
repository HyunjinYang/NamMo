using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : BaseAttack
{
    public enum AttackShape
    {
        Box,
        Circle,
    }
    
    [Header("�������� ����")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private float _radius;
    private AttackShape _attackShape = AttackShape.Box;
    private Collider2D[] _hits;

    bool _isAttackerFacingRight = false;
    protected override void UpdateAttack()
    {
        if (_attacker == null) return;
        if (_attackerType == AttackerType.Player)
        {
            _isAttackerFacingRight = _attacker.GetComponent<PlayerController>().GetPlayerMovement().IsFacingRight;
        }
        else if (_attackerType == AttackerType.Enemy)
        {
            // TODO
            _isAttackerFacingRight = _attacker.GetComponent<Enemy.Enemy>().IsFacingRight;
        }
    }
    public void SetAttackShape(AttackShape attackShape) { _attackShape = attackShape; }
    public void SetAttackRange(Vector2 offset, Vector2 size)
    {
        _offset = offset;
        _size = size;
    }
    public void SetAttackRange(Vector2 offset, float radius)
    {
        _offset = offset;
        _radius = radius;
    }
    public void Attack()
    {
        Vector2 offset = _offset;
        if (!_isAttackerFacingRight) offset.x = -_offset.x;

        if (_attackShape == AttackShape.Box)
        {
            _hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y) + offset, _size, 0);
        }
        else if (_attackShape == AttackShape.Circle)
        {
            _hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y) + offset, _radius);
        }

        if (_attackerType == AttackerType.Player)
        {
            foreach (Collider2D collision in _hits)
            {
                if (collision.gameObject == _attacker) continue;
                TryHit(collision.gameObject);
            }
        }
        else
        {
            PlayerController pc = null;
            BlockArea ba = null;
            foreach (Collider2D collision in _hits)
            {
                if (collision.gameObject == _attacker) continue;
                if (collision.gameObject.GetComponent<PlayerController>()) pc = collision.gameObject.GetComponent<PlayerController>();
                if (collision.gameObject.GetComponent<BlockArea>()) ba = collision.gameObject.GetComponent<BlockArea>();
            }
            // ��� ����, �÷��̾� ���� �� �κ� �� ���� ���
            if (pc != null && ba != null)
            {
                float blockAreaPosX = ba.transform.position.x;
                float playerPosX = pc.transform.position.x;
                float attackerPosX = _attacker.transform.position.x;
                if((blockAreaPosX > playerPosX && blockAreaPosX < attackerPosX)
                    || (blockAreaPosX > attackerPosX && blockAreaPosX < playerPosX))
                {
                    ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
                }
                else
                {
                    TryHit(pc.gameObject);
                }
            }
            // ������ ���� ���
            else if (pc == null && ba != null)
            {
                ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
            }
            // �÷��̾ ���� ���
            else if (pc != null && ba == null)
            {
                TryHit(pc.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 offset = _offset;
        if (!_isAttackerFacingRight) offset.x = -_offset.x;

        Gizmos.color = UnityEngine.Color.blue;
        if (_attackShape == AttackShape.Box)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + offset, _size);
        }
        else if (_attackShape == AttackShape.Circle)
        {
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + offset, _radius);
        }
    }
}
