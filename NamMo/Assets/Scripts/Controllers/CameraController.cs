using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _vCam;
    [SerializeField] private CinemachineImpulseListener _cinemachineImpulseListener;
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private LockCameraY _lockCamY;
    [SerializeField] private float _yOffset;
    [SerializeField] public Define.CameraMode CameraMode;
    private GameObject _target;
    private CinemachineFramingTransposer _vCam_Transposer;
    private PlayerController _pc;
    private float _targetPosY;
    private float _currPosY;

    private Coroutine _cameraZoomCoroutine = null;
    private void Awake()
    {
        if (_camera == null) _camera = Camera.main;

        _vCam_Transposer = _vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void Update()
    {
        if (_pc.GetPlayerMovement().IsFacingRight) _vCam_Transposer.m_TrackedObjectOffset = Vector3.right;
        else _vCam_Transposer.m_TrackedObjectOffset = Vector3.left;

        _currPosY = Mathf.Lerp(_currPosY, _targetPosY, 0.02f);
        _lockCamY.PosY = _currPosY + _yOffset;
    }
    public void SetTargetInfo(GameObject target)
    {
        _target = target;
        _pc = _target.GetComponent<PlayerController>();
        _vCam.Follow = _target.transform;

        _targetPosY = _target.GetComponentInChildren<FloorDivideDetector>().transform.position.y;
        _currPosY = _targetPosY;
    }
    public void SetCameraOffsetY(float posY)
    {
        _targetPosY = posY;
    }
    public void ShakeCamera(float force)
    {
        _cinemachineImpulseSource.GenerateImpulseWithForce(force);
    }
    public void ZoomCamera()
    {
        if (_cameraZoomCoroutine != null)
        {
            StopCoroutine(_cameraZoomCoroutine);
            _vCam.m_Lens.OrthographicSize = Mathf.Min(_vCam.m_Lens.OrthographicSize + 0.3f, 12f);
        }
        _cameraZoomCoroutine = StartCoroutine(CoZoomCamera());
    }
    IEnumerator CoZoomCamera()
    {
        while (true)
        {
            if (_vCam.m_Lens.OrthographicSize <= 11f)
            {
                _vCam.m_Lens.OrthographicSize = 11f;
                break;
            }
            _vCam.m_Lens.OrthographicSize -= 0.1f;
            yield return new WaitForSecondsRealtime(0.03f);
        }
        while (true)
        {
            if (_vCam.m_Lens.OrthographicSize >= 12f)
            {
                _vCam.m_Lens.OrthographicSize = 12f;
                break;
            }
            _vCam.m_Lens.OrthographicSize += 0.1f;
            yield return new WaitForSecondsRealtime(0.03f);
        }
    }
}
