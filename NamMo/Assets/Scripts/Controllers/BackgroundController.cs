using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float _parallaxEffect;
    private float _startPos;
    private float _length;
    private GameObject _cam;

    private void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;

        _cam = Camera.main.gameObject;
    }
    private void FixedUpdate()
    {
        float distance = _cam.transform.position.x * _parallaxEffect;
        float movement = _cam.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        if(movement > _startPos + _length)
        {
            _startPos += _length;
        }
        else if(movement < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
