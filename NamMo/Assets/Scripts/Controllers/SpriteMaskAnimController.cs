using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskAnimController : MonoBehaviour
{
    [Range(0.02f, 0.2f)]
    [SerializeField] private float _spriteChangeTime;
    [SerializeField] private SpriteMask _spriteMask;
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
    private int _idx = 0;
    private void Start()
    {
        StartCoroutine(CoChangeSpriteShape());
    }
    IEnumerator CoChangeSpriteShape()
    {
        while (true)
        {
            _spriteMask.sprite = _sprites[_idx];
            _idx = (_idx + 1) % _sprites.Count;
            yield return new WaitForSeconds(_spriteChangeTime);
        }
    }
}
