using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaterialValueChanger : MonoBehaviour
{
    [SerializeField] private Vector2 _res;
    private Material _mat;
    private Camera _cam;

    public static float FadeOutDistance = 0;
    private static bool _fadeOut = false;
    private static bool _fadeOutDistanceValueChangedNowFrame = false;
    public static void StartFadeOut()
    {
        _fadeOut = true;
    }
    public static void EndFadeOut()
    {
        _fadeOut = false;
    }
    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _cam = Camera.main;
        _res = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }
    private void Update()
    {
        Vector2 playerScreenPos = _cam.WorldToScreenPoint(Managers.Scene.CurrentScene.Player.transform.position);

        _mat.SetVector("_PlayerScreenPosition", playerScreenPos);
        //_mat.SetVector("_Resolution", new Vector2(Screen.currentResolution.width, Screen.currentResolution.height));
        //_mat.SetFloat("_PlayerViewRangeDistance", Screen.currentResolution.height / 4);

        _mat.SetVector("_Resolution", new Vector2(_res.x, _res.y));
        //_mat.SetFloat("_PlayerViewRangeDistance", _res.y / 4 + 5);
        _mat.SetFloat("_PlayerViewRangeDistance", 0);

        if (_fadeOut)
        {
            if (_fadeOutDistanceValueChangedNowFrame == false)
            {
                _fadeOutDistanceValueChangedNowFrame = true;
                FadeOutDistance += 5f;
            }
        }
        else
        {
            FadeOutDistance = 0;
        }

        _mat.SetFloat("_FadeOutDistance", FadeOutDistance);
    }
    private void LateUpdate()
    {
        if (_fadeOutDistanceValueChangedNowFrame) _fadeOutDistanceValueChangedNowFrame = false;
    }
}
