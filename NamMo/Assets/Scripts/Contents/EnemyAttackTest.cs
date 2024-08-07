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
    private void Start()
    {
        StartCoroutine(CoSpawnEnemyAttack());
    }
    IEnumerator CoSpawnEnemyAttack()
    {
        while (true)
        {
            GameObject enemyAttack = Instantiate(_enemyAttackPrefab, Vector3.down * 2, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}
