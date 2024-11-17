using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;


public class EffectManager
{
    public void Init()
    {
        
    }

    public void PlayOnShot(GameObject _particleSystem, Transform _transform)
    {
        GameObject cur = Object.Instantiate(_particleSystem, _transform.position, _transform.rotation);
        
        cur.GetComponent<ParticleSystem>().Play();
    }
}
