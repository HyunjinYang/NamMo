using System;
using System.Collections;
using System.Collections.Generic;
using NamMo;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class RangedEnemy : Enemy
    {
        public Action OnRangeAttack;
        public Action OnEndRangeAttack;
        
        [SerializeField] private float AttackTime;
        
        [SerializeField] private GameObject archr;
        [SerializeField] private EnemyBlockArea _enemyBlockArea;
        private Animator _animator;
        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SceneLinkedSMB<RangedEnemy>.Initialise(_animator, this);
            _enemyBlockArea.SetAttackInfo(gameObject, 2);

        }

        enum State
        {
            Patrol,
            RangeAttack,
            MelAttack,
            None
        }

        [SerializeField] private State _state = State.None;

        public override void Behavire(float distance)
        {
            if (distance >= 6.5f)
            {
                Patrol();
                _state = State.Patrol;
            }
            else if (distance < 6.5f && distance >= 3.5f)
            {
                RangeAttackInit();
                _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            }
            else if (distance < 3.5f)
            {
                MelAttackInit();
                _enemyMovement.DirectCheck(gameObject.transform.position.x, Managers.Scene.CurrentScene.Player.transform.position.x);
            }
        }


        public void RangeAttack()
        {
            GameObject cur =Instantiate(archr, gameObject.transform.position, transform.rotation);
            cur.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 1f, 4, Managers.Scene.CurrentScene.Player.gameObject);
            cur.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (cur)
                {
                    Managers.Resource.Destroy(cur);
                }
            });
        }
        

        private void RangeAttackInit()
        {
            if (_state == State.RangeAttack)
                return;
            _state = State.RangeAttack;
            Debug.Log("RangeAttack!");
            OnRangeAttack.Invoke();
            _enemyMovement.OnWalk(0f);
            _enemyMovement._isPatrol = false;
            _enemyMovement._isAttack = true;
        }

        private void MelAttackInit()
        {
            if (_enemyMovement._isPatrol)
                return;
            if (_state == State.MelAttack)
            {
                if (!_enemyMovement._isAttack)
                {
                    _enemyMovement._isAttack = true;
                    _enemyMovement.OnWalk(0f);
                }

                return;
            }
            Onattack.Invoke();
            _enemyMovement.OnWalk(0f);
            _enemyMovement._isPatrol = false;
            _enemyMovement._isAttack = true;
            _state = State.MelAttack;
            Debug.Log("MelAttack");
        }

        public void MelAttack()
        {
            StartCoroutine(CoAttack());
        }
        public void Patrol()
        {
            if (_state == State.Patrol)
                return;
            OnEndattack.Invoke();
            _enemyMovement._isPatrol = true;
        }


        public void SetRangeAttack(bool isActivate)
        {
            if (_state != State.RangeAttack)
            {
                _enemyMovement._isAttack = isActivate;
                OnEndRangeAttack.Invoke();
            }
        }

        public void SetMelAttack(bool isActivate)
        {
            //_enemyBlockArea.DeActiveBlockArea();
            if (_state != State.MelAttack)
            {
                _enemyMovement._isAttack = isActivate;
                OnEndattack.Invoke();
            }
        }
        public void SetHit(bool isActivate)
        {
            _enemyMovement._isHit = isActivate;
            OnEndHit.Invoke();
        }

        public void Dead()
        {
            Destroy(_enemyMovement.gameObject);
        }


        IEnumerator CoAttack()
        {
            yield return new WaitForSeconds(AttackTime);
            _enemyBlockArea.ActiveAttackArea();
        }

    }
}