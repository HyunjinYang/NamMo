using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHole : MonoBehaviour
{
    [SerializeField] private bool _viewOn = true;
    private float _scale = 1f;
    private void Start()
    {
        StartCoroutine(TestChangeSize());
    }
    IEnumerator TestChangeSize()
    {
        while (true)
        {
            if (_viewOn)
            {
                if (GetComponent<Collider2D>().enabled == false)
                {
                    GetComponent<Collider2D>().enabled = true;
                }
                _scale += 0.15f;
                if (_scale >= 1f)
                {
                    _scale = 1f;
                }
                transform.localScale = Vector3.one * _scale;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                _scale -= 0.15f;
                if (_scale <= 0f)
                {
                    _scale = 0f;
                    if (GetComponent<Collider2D>().enabled == true)
                    {
                        GetComponent<Collider2D>().enabled = false;
                    }
                }
                transform.localScale = Vector3.one * _scale;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
