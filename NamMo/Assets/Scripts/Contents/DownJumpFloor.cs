using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJumpFloor : MonoBehaviour
{
    public void DeActiveShortTime()
    {
        GetComponent<Collider2D>().isTrigger = true;
        StartCoroutine(CoDeActiveShortTime());
    }
    IEnumerator CoDeActiveShortTime()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().isTrigger = false;
    }
}
