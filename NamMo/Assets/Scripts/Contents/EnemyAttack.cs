using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float _speed;
    float _damage = 2f;
    private bool _blockCheck = false;
    private void Start()
    {
        //Destroy(gameObject, 1.5f);
    }
    /*private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }*/
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
    //    BlockArea ba = collision.gameObject.GetComponent<BlockArea>();
    //    if (pc == null && ba == null) return;
    //    if (ba)
    //    {
    //        if (_blockCheck == false)
    //        {
    //            _blockCheck = true;
    //            ba.OnBlockAreaTriggerEntered.Invoke(gameObject);
    //        }
    //    }
    //    if (pc)
    //    {
    //        if (_blockCheck == false)
    //        {
    //            List<Collider2D> results = new List<Collider2D>();
    //            ContactFilter2D filter = new ContactFilter2D().NoFilter();
    //            GetComponent<Collider2D>().OverlapCollider(filter, results);
    //            foreach (Collider2D c in results)
    //            {
    //                BlockArea blockArea = c.gameObject.GetComponent<BlockArea>();
    //                if (blockArea)
    //                {
    //                    _blockCheck = true;
    //                    blockArea.OnBlockAreaTriggerEntered.Invoke(gameObject);
    //                    break;
    //                }
    //            }
    //        }
    //        pc.GetPlayerCombatComponent().GetDamaged(_damage, transform.position);
    //    }
    //    if (_blockCheck)
    //    {
    //        StartCoroutine(CoRefreshBlockCheck());
    //    }
    //}
    IEnumerator CoRefreshBlockCheck()
    {
        yield return new WaitForEndOfFrame();
        _blockCheck = false;
    }
}
