using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJumpFloor : MonoBehaviour
{
    private bool _ignoreGroundCheck = false;
    public bool IgnoreGroundCheck { get { return _ignoreGroundCheck; } }
    public void ChangeColliderMaskShortTime()
    {
        GetComponent<PlatformEffector2D>().colliderMask = ~LayerMask.GetMask("Player");
        _ignoreGroundCheck = true;
        StartCoroutine(CoChangeColliderMaskShortTime());
    }
    IEnumerator CoChangeColliderMaskShortTime()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        _ignoreGroundCheck = false;
        GetComponent<PlatformEffector2D>().colliderMask = -1;
    }
}
