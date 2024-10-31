using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDivideTrigger : MonoBehaviour
{
    [SerializeField] private FloorDivideTrigger _downFloor;
    public void SetCameraOffset()
    {
        if (Camera.main == null) return;
        Camera.main.gameObject.GetComponent<CameraController>().SetCameraOffsetY(transform.position.y);
    }
    public void SetCameraOffsetDownFloor()
    {
        if (_downFloor == null) return;
        _downFloor.SetCameraOffset();
    }
}
