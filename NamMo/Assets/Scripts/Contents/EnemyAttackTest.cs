using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTest : MonoBehaviour
{
    public GameObject _enemyAttackPrefab;
    public float _speed;
    public float _maxX;
    public float _minX;
    private GameObject _enemyAttack;
    public int _dir = 1;
    private void Update()
    {
        if (_enemyAttack == null)
        {
            _enemyAttack = Instantiate(_enemyAttackPrefab, Vector3.down * 2, Quaternion.identity);
            _dir = 1;
        }
        _enemyAttack.transform.position += Vector3.right * _speed * Time.deltaTime * _dir;
        if (_enemyAttack.transform.position.x >= _maxX && _dir == 1) _dir = -1;
        else if (_enemyAttack.transform.position.x <= _minX && _dir == -1) _dir = 1;
    }
}
