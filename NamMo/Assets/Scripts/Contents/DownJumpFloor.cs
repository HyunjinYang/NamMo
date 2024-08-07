using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJumpFloor : MonoBehaviour
{
    public void ChangeColliderMaskShortTime()
    {
        GetComponent<PlatformEffector2D>().colliderMask = ~LayerMask.GetMask("Player");
        StartCoroutine(CoChangeColliderMaskShortTime());
    }
    IEnumerator CoChangeColliderMaskShortTime()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlatformEffector2D>().colliderMask = -1;
    }
}
