using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ViewRangeDeform : MonoBehaviour
{
    [SerializeField] private float _deformSpeed;
    private MeshFilter _meshFilter;
    private Mesh _mesh;

    Vector3[] _originVertices;
    Vector3[] _vertices;
    bool[] _deformCheck;

    float[] _dirX = new float[12] { 0, 0.5f, 0.86f, 1, 0.86f, 0.5f, 0, -0.5f, -0.86f, -1, -0.86f, -0.5f };
    float[] _dirY = new float[12] { 1, 0.86f, 0.5f, 0, -0.5f, -0.86f, -1, -0.86f, -0.5f, 0, 0.5f, 0.86f };
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;

        GetVertices();
    }
    private void Update()
    {
        DeformVertices();
        UpdateVertices();
    }
    private void GetVertices()
    {
        _originVertices = new Vector3[_mesh.vertices.Length];
        _vertices = new Vector3[_mesh.vertices.Length];
        _deformCheck = new bool[_mesh.vertices.Length];
        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            _originVertices[i] = _mesh.vertices[i];
            _originVertices[i].z = 0;
            _vertices[i] = _mesh.vertices[i];
        }
    }
    private void DeformVertices()
    {
        for (int i = 0; i < _vertices.Length; i++)
        {
            _deformCheck[i] = false;
        }
        for (int i = 0; i < 12; i++) // 12방향 ray 탐색
        {
            RaycastHit2D hit;
            float rayDist = 5f;
            hit = Physics2D.Raycast(transform.position, new Vector2(_dirX[i], _dirY[i]), rayDist, LayerMask.GetMask("Ground")); // Ground Layer만 탐색
            if (hit)
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);

                // Ray에 탐색된 오브젝트 내의 가장 가까운 좌표를 추출
                Vector2 closestPos = hit.collider.ClosestPoint(transform.position);
                // 탐색된 가장 가까운 좌표를 local로 변환
                Vector2 localClosestPos = transform.InverseTransformPoint(closestPos);
                // 충돌 지점을 local 좌표계로 변환
                Vector2 localHitPoint = transform.InverseTransformPoint(hit.point);
                // 위의 두 좌표를 통해 직선의 벡터를 구한다
                Vector2 localHitObjectVector = localClosestPos - localHitPoint;
                if (localHitObjectVector.magnitude > 0.03f) localHitObjectVector = localHitObjectVector.normalized;
                // 내적을 통해 투영 길이를 구한다
                float projection = Vector2.Dot(-localHitPoint, localHitObjectVector);
                // 최단 벡터를 구한다. (원점에서 투영된 점까지의 벡터)
                Vector2 projectedPoint = localHitPoint + localHitObjectVector * projection;
                for (int j = 0; j < _vertices.Length; j++)
                {
                    // 충돌 지점까지의 벡터와 모든 정점들까지의 벡터를 검사
                    float value = Vector2.Angle(new Vector2(localHitPoint.x, localHitPoint.y), new Vector2(_originVertices[j].x, _originVertices[j].y));
                    float maxAngle = 15f;
                    // 각도가 15도 이상이면 스킵, 다른 영역에 맡긴다.
                    if (value > maxAngle) continue;

                    // 최단 벡터와 정점까지의 벡터의 사이각을 구한다.
                    float angle = Vector2.Angle(projectedPoint, new Vector2(_originVertices[j].x, _originVertices[j].y));

                    // 삼각함수 응용하면 이런 식을 얻을 수 있음
                    float vertexLen = (projectedPoint.magnitude) / Mathf.Cos(angle * Mathf.Deg2Rad);
                    vertexLen /= rayDist;
                    vertexLen = Mathf.Clamp(vertexLen, 0, 1);
                    _vertices[j] = Vector3.Slerp(_vertices[j], _originVertices[j] * vertexLen, _deformSpeed);
                    _deformCheck[j] = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + new Vector3(_dirX[i], _dirY[i]) * 5f, Color.red);
            }
        }
        for (int i = 0; i < _vertices.Length; i++)
        {
            if (_deformCheck[i] == false)
            {
                _vertices[i] = Vector3.Slerp(_vertices[i], _originVertices[i], _deformSpeed);
            }
        }
    }
    private void UpdateVertices()
    {
        _mesh.vertices = _vertices;
        _mesh.RecalculateBounds();
        //_mesh.RecalculateNormals();
        //_mesh.RecalculateTangents();
    }
}
