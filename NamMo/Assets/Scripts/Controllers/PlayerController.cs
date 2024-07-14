using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    float _speed = 10.0f;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        float inputVal = Input.GetAxis("Horizontal");

        Vector2 moveVec = new Vector2(inputVal, 0) * _speed * Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + moveVec);
    }
}
