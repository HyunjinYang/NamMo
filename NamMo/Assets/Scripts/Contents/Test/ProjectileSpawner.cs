using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _speed;
    Coroutine _spawnProjectileCoroutine = null;
    public void StartSpawnProjectile()
    {
        _spawnProjectileCoroutine = StartCoroutine(CoSpawnProjectile());
    }
    public void StopSpawnProjectile()
    {
        StopCoroutine(_spawnProjectileCoroutine);
    }
    IEnumerator CoSpawnProjectile()
    {
        while (true)
        {
            GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<BaseProjectile>().SetProjectileInfo(Managers.Scene.CurrentScene.Player.gameObject, _speed, 1f, null);
            yield return new WaitForSeconds(3f);
        }
    }
}
