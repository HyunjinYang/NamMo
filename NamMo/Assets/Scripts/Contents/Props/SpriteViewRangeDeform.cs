using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.Animation;

public class SpriteViewRangeDeform : MonoBehaviour
{
    [SerializeField] private SpriteSkin _spriteSkin;
    [Range(0,1)]
    [SerializeField] private float _deformSpeed;
    [SerializeField] private float _rayDist;
    [SerializeField] private float _noDetectRevision;
    [SerializeField] private float _detectRevision;
    [SerializeField] private Vector2 _randomMove;

    [Header("Light")]
    [SerializeField] private Light2D _light;

    private List<Transform> _viewRangeTransforms = new List<Transform>();
    private List<Transform> _viewRangeOriginTransforms = new List<Transform>();
    private List<bool> _detectCheck = new List<bool>();
    private Vector3[] _targetPoses;
    void Start()
    {
        _targetPoses = new Vector3[_spriteSkin.boneTransforms.Length];
        for (int i = 0; i < _spriteSkin.boneTransforms.Length; i++)
        {
            _viewRangeTransforms.Add(_spriteSkin.boneTransforms[i]);
            _detectCheck.Add(false);
            _targetPoses[i] = Vector3.zero;
        }
        for (int i = 0; i < _viewRangeTransforms.Count; i++)
        {
            GameObject go = new GameObject($"Bone_OriginPos{i + 1}");
            go.transform.position = _viewRangeTransforms[i].transform.position;
            go.transform.SetParent(transform);
            _viewRangeOriginTransforms.Add(go.transform);
        }
    }
    void Update()
    {
        DetectGround();
    }
    private void DetectGround()
    {
        for(int i = 0 ; i < _viewRangeOriginTransforms.Count; i++)
        {
            RaycastHit2D hit;
            Vector3 direction = (new Vector3(_viewRangeOriginTransforms[i].position.x, _viewRangeOriginTransforms[i].position.y) - transform.position).normalized;
            hit = Physics2D.Raycast(transform.position, direction, _rayDist, LayerMask.GetMask("Ground")); // Ground Layer¸¸ Å½»ö
            if (hit)
            {
                Debug.DrawLine(transform.position, hit.point + new Vector2(direction.x, direction.y) * _detectRevision, Color.yellow);
                Debug.DrawLine(transform.position, hit.point, Color.green);
                _targetPoses[i] = hit.point + new Vector2(direction.x, direction.y) * _detectRevision;
                _detectCheck[i] = true;
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direction * _rayDist, Color.red);
                _targetPoses[i] = transform.position + direction * _noDetectRevision;
                _detectCheck[i] = false;
            }
            float randX = Random.Range(-_randomMove.x, _randomMove.x);
            float randY = Random.Range(-_randomMove.y, _randomMove.y);
            Vector3 randVec = new Vector2(randX, randY);
            _targetPoses[i] += randVec;

            if (_detectCheck[i])
            {
                Vector3 currPos = _viewRangeTransforms[i].position;
                _viewRangeTransforms[i].position = Vector3.Lerp(currPos, _targetPoses[i], _deformSpeed);
            }
        }
        for (int i = 0; i < _viewRangeOriginTransforms.Count; i++)
        {
            if (_detectCheck[i]) continue;
            int cnt = _viewRangeOriginTransforms.Count;
            int leftIdx = (i - 1 + cnt) % cnt;
            int rightIdx = (i + 1) % cnt;
            Vector3 currPos = _viewRangeTransforms[i].position;
            if ((_detectCheck[leftIdx] == false && _detectCheck[rightIdx] == false) || i != 0)
            {
                _viewRangeTransforms[i].position = Vector3.Lerp(currPos, _targetPoses[i], _deformSpeed);
                continue;
            }

            Vector3 leftPos = _targetPoses[leftIdx];
            Vector3 rightPos = _targetPoses[rightIdx];

            Vector3 targetPos = (leftPos + rightPos) / 2;

            _viewRangeTransforms[i].position = Vector3.Lerp(currPos, targetPos, _deformSpeed);
        }
        //_light.SetShapePath(_targetPoses);
    }
}
