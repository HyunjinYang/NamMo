using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHoleDetector : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteMaskInteraction _spriteMaskInteraction;
    private Collider2D _collider;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMaskInteraction = _spriteRenderer.maskInteraction;
        _collider = GetComponent<Collider2D>();

        if (_spriteMaskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            _collider.excludeLayers = LayerMask.GetMask("Player");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ViewHole>() == null) return;

        if (_spriteMaskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            _collider.excludeLayers = ~-1;
        }
        else if (_spriteMaskInteraction == SpriteMaskInteraction.VisibleOutsideMask)
        {
            _collider.excludeLayers = LayerMask.GetMask("Player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ViewHole>() == null) return;

        if (_spriteMaskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            _collider.excludeLayers = LayerMask.GetMask("Player");
        }
        else if (_spriteMaskInteraction == SpriteMaskInteraction.VisibleOutsideMask)
        {
            _collider.excludeLayers = ~-1;
        }
    }
}
