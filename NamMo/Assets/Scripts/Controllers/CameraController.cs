using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _target;
    [SerializeField] 
    [Range(0, 1)] private float _followSpeed_Horizontal;
    [SerializeField]
    [Range(0, 1)] private float _followSpeed_Vertical;
    [SerializeField] private Vector2 _posOffset;
    private float _flipOffset;
    private void Awake()
    {
        if (_camera == null) _camera = Camera.main;
        _flipOffset = 3f;

        PlayerMovement pm = _target.GetComponent<PlayerMovement>();
        if (pm)
        {
            pm.OnFlip += RefreshFlipOffset;
        }
    }
    private void FixedUpdate()
    {
        if (_target) FollowTarget();
    }
    private void FollowTarget()
    {
        Vector2 targetPos = new Vector2(_target.transform.position.x, _target.transform.position.y) + _posOffset + new Vector2(_flipOffset, 0);
        float currX = _camera.transform.position.x;
        float currY = _camera.transform.position.y;
        float targetX = targetPos.x;
        float targetY = targetPos.y;

        float posX = Mathf.Lerp(currX, targetX, _followSpeed_Horizontal);
        float posY = Mathf.Lerp(currY, targetY, _followSpeed_Vertical);

        _camera.transform.position = new Vector3(posX, posY, -10f);
    }
    private void RefreshFlipOffset(bool isFacingRight)
    {
        if (isFacingRight) _flipOffset = 3f;
        else _flipOffset = -3f;
    }
}
