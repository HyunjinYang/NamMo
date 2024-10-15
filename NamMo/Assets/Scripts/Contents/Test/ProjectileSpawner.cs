using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackStrength = 1;
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
            projectile.GetComponent<BaseProjectile>().SetAttackInfo(gameObject, 1f, _attackStrength, _speed, Managers.Scene.CurrentScene.Player.gameObject);
            projectile.GetComponent<BaseProjectile>().OnHitted += ((go) =>
            {
                if (projectile)
                {
                    Managers.Resource.Destroy(projectile);
                }
            });
            yield return new WaitForSeconds(3f);
        }
    }
}
