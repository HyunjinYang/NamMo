using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Heart : UI_Base
{
    private float _currentValue = 1;
    enum Images
    {
        Image_Heart
    }
    public override void Init()
    {
        if (_init) return;
        _init = true;
        Bind<Image>(typeof(Images));
    }
    public void SetHeart(float value)
    {
        if (_currentValue == value) return;
        float duration = Mathf.Abs(_currentValue - value) / 3f;
        Get<Image>((int)Images.Image_Heart).DOFillAmount(value, duration);
        _currentValue = value;
    }
}
