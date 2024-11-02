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

    [SerializeField] private float _limitDistance;

    [Header("Light")]
    [SerializeField] private Light2D _light;
    [SerializeField] private Light2D _enemyLight;
    [SerializeField] private Light2D _circleLight;

    [Header("Enemy Light")]
    [SerializeField] private float _detectReviseValue;
    [SerializeField] private float _noDetectReviseValue;

    private List<Transform> _viewRangeTransforms = new List<Transform>();
    private List<Transform> _viewRangeOriginTransforms = new List<Transform>();
    private List<bool> _detectCheck = new List<bool>();
    private Vector3[] _targetPoses;
    private Vector3[] _lightPoses;
    private Vector3[] _targetLightPoses;
    private Vector3[] _lightPoses_Enemy;
    private Vector3[] _targetLightPoses_Enemy;
    private Vector3[] _circleLightPoses;
    void Start()
    {
        //_light.transform.SetParent(null);
        //_light.transform.position = Vector3.zero;

        _circleLightPoses = _circleLight.shapePath;
        _enemyLight.SetShapePath(_circleLightPoses);

        _targetPoses = new Vector3[_spriteSkin.boneTransforms.Length];
        _lightPoses = new Vector3[_spriteSkin.boneTransforms.Length];
        _targetLightPoses = new Vector3[_spriteSkin.boneTransforms.Length];
        _lightPoses_Enemy = new Vector3[_spriteSkin.boneTransforms.Length];
        _targetLightPoses_Enemy = new Vector3[_spriteSkin.boneTransforms.Length];
        for (int i = 0; i < _spriteSkin.boneTransforms.Length; i++)
        {
            _viewRangeTransforms.Add(_spriteSkin.boneTransforms[i]);
            _detectCheck.Add(false);
            _targetPoses[i] = Vector3.zero;
            _lightPoses[i] = _circleLightPoses[i];
            _targetLightPoses[i] = _circleLightPoses[i];
            _lightPoses_Enemy[i] = _circleLightPoses[i];
            _targetLightPoses_Enemy[i] = _circleLightPoses[i];
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
                Vector3 dir = (_viewRangeOriginTransforms[i].position - transform.position).normalized;

                _targetPoses[i] = hit.point + new Vector2(direction.x, direction.y) * _detectRevision;
                _targetLightPoses[i] = transform.InverseTransformPoint(hit.point) / 5;

                Vector3 enemyLightV = (new Vector3(hit.point.x, hit.point.y) - transform.position);
                if(enemyLightV.magnitude > 4f)
                {
                    _targetLightPoses_Enemy[i] = _circleLightPoses[i] * _noDetectReviseValue;
                }
                else
                {
                    _targetLightPoses_Enemy[i] = _circleLightPoses[i] * enemyLightV.magnitude / _detectReviseValue;
                }

                _detectCheck[i] = true;
                if (_targetLightPoses[i].magnitude < _limitDistance)
                {
                    //_targetPoses[i] = transform.position; /* + dir * _noDetectRevision / 5;*/
                    _targetLightPoses[i] = _circleLightPoses[i] / 10f;
                    _targetLightPoses_Enemy[i] = _circleLightPoses[i] / _detectReviseValue / 2f;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direction * _rayDist, Color.red);
                Vector3 dir = (_viewRangeOriginTransforms[i].position - transform.position).normalized;
                _targetPoses[i] = _viewRangeOriginTransforms[i].position + dir * _noDetectRevision;
                //_lightPoses[i] = (/*transform.position / 5 + */_circleLightPoses[((32 - i + 8) + 32) % 32]);
                _targetLightPoses[i] = _circleLightPoses[i];
                _targetLightPoses_Enemy[i] = _circleLightPoses[i] * _noDetectReviseValue;
                _detectCheck[i] = false;
            }
            float randX = Random.Range(-_randomMove.x, _randomMove.x);
            float randY = Random.Range(-_randomMove.y, _randomMove.y);
            Vector3 randVec = new Vector2(randX, randY);
            _targetPoses[i] += randVec;

            if (_detectCheck[i])
            {
                Vector3 currPos = _viewRangeTransforms[i].position;
                Vector3 currLightPos = _lightPoses[i];
                Vector3 currEnemyLightPos = _lightPoses_Enemy[i];
                _viewRangeTransforms[i].position = Vector3.Lerp(currPos, _targetPoses[i], _deformSpeed);
                _lightPoses[i] = Vector3.Lerp(currLightPos, _targetLightPoses[i], _deformSpeed);
                _lightPoses_Enemy[i] = Vector3.Lerp(currEnemyLightPos, _targetLightPoses_Enemy[i], _deformSpeed);
            }
        }
        for (int i = 0; i < _viewRangeOriginTransforms.Count; i++)
        {
            if (_detectCheck[i]) continue;
            int cnt = _viewRangeOriginTransforms.Count;
            int leftIdx = (i - 1 + cnt) % cnt;
            int rightIdx = (i + 1) % cnt;
            Vector3 currPos = _viewRangeTransforms[i].position;
            Vector3 currLightPos = _lightPoses[i];
            Vector3 currEnemyLightPos = _lightPoses_Enemy[i];
            if ((_detectCheck[leftIdx] == false && _detectCheck[rightIdx] == false))
            {
                _viewRangeTransforms[i].position = Vector3.Lerp(currPos, _targetPoses[i], _deformSpeed);
                _lightPoses[i] = Vector3.Lerp(currLightPos, _targetLightPoses[i], _deformSpeed);
                _lightPoses_Enemy[i] = Vector3.Lerp(currEnemyLightPos, _targetLightPoses_Enemy[i], _deformSpeed);
                continue;
            }

            Vector3 leftPos = _targetPoses[leftIdx];
            Vector3 rightPos = _targetPoses[rightIdx];

            Vector3 targetPos = (leftPos + rightPos) / 2;

            _viewRangeTransforms[i].position = Vector3.Lerp(currPos, targetPos, _deformSpeed);
            _lightPoses[i] = Vector3.Lerp(currLightPos, _targetLightPoses[i], _deformSpeed);
            _lightPoses_Enemy[i] = Vector3.Lerp(currEnemyLightPos, _targetLightPoses_Enemy[i], _deformSpeed);
        }
        _light.SetShapePath(_targetLightPoses);
        _enemyLight.SetShapePath(_lightPoses_Enemy);
    }
}
