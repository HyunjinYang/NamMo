using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _transform1;
    [SerializeField] private Transform _transform2;
    [SerializeField] private float _speed;
    private Transform _targetTransform;

    bool _reached = false;
    private void Start()
    {
        _targetTransform = _transform1;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, _speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, _targetTransform.position) < 0.1f && _reached == false)
        {
            _reached = true;
            ChangeTargetTransform();
        }
    }
    private void ChangeTargetTransform()
    {
        _reached = false;
        if (_targetTransform == _transform1) _targetTransform = _transform2;
        else _targetTransform = _transform1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        player.gameObject.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        player.gameObject.transform.SetParent(null);
    }
}
