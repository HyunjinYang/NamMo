using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBlockArea : BlockArea
    {
        public bool _isHit = false;
        private bool _blockCheck = false;
        public Action _groggy; 
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            BlockArea ba = collision.gameObject.GetComponent<BlockArea>();
            if (pc == null && ba == null) return;
            if (ba)
            {
                if (_blockCheck == false)
                {
                    _blockCheck = true;
                    //ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
                }
            }
            if (pc)
            {
                if (_blockCheck == false)
                {
                    List<Collider2D> results = new List<Collider2D>();
                    ContactFilter2D filter = new ContactFilter2D().NoFilter();
                    GetComponent<Collider2D>().OverlapCollider(filter, results);
                    foreach (Collider2D c in results)
                    {
                        BlockArea blockArea = c.gameObject.GetComponent<BlockArea>();
                        if (blockArea)
                        {
                            _blockCheck = true;
                            blockArea.OnBlockAreaTriggerEntered.Invoke(gameObject);
                            if (_groggy != null)
                                _groggy.Invoke();
                            break;
                        }
                    }
                }
                pc.GetPlayerCombatComponent().GetDamaged(1, transform.position);
            }
            if (_blockCheck)
            {
                StartCoroutine(CoRefreshBlockCheck());
            }
            DeActiveBlockArea();
        }
        IEnumerator CoRefreshBlockCheck()
        {
            yield return new WaitForEndOfFrame();
            _blockCheck = false;
        }
        
    }
}