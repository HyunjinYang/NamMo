using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArea : MonoBehaviour
{
    [SerializeField] private GameObject _testImage;
    private Collider2D _collider;
    public Action<GameObject> OnBlockAreaTriggerEntered;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;

        _testImage.SetActive(false);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAttack>() == null) return;
        //if (OnBlockAreaTriggerEntered != null) OnBlockAreaTriggerEntered.Invoke(collision.gameObject);
    }
    Vector3 _localPos;
    Vector3 _localRot;
    public void SetDirection(Define.Direction direction)
    {
        PlayerController pc = Managers.Scene.CurrentScene.Player;
        switch (direction)
        {
            case Define.Direction.Up:
                _localPos = new Vector3(0, 1.2f);
                break;
            case Define.Direction.RightUp:
                _localPos = new Vector3(0.72f, 0.72f);
                break;
            case Define.Direction.Right:
                _localPos = new Vector3(0.8f, 0);
                break;
            case Define.Direction.RightDown:
                _localPos = new Vector3(0.72f, -0.72f);
                break;
            case Define.Direction.Down:
                _localPos = new Vector3(0, -1.2f);
                break;
            case Define.Direction.LeftDown:
                _localPos = new Vector3(-0.72f, -0.72f);
                break;
            case Define.Direction.Left:
                _localPos = new Vector3(-0.8f, 0);
                break;
            case Define.Direction.LeftUp:
                _localPos = new Vector3(-0.72f, 0.72f);
                break;
        }
        float rot = 90f + (int)direction * 45f;
        _localRot = new Vector3(0, 0, rot);
    }
    public void ActiveBlockArea()
    {
        _collider.enabled = true;

        transform.localPosition = _localPos;
        transform.localEulerAngles = _localRot;

        _testImage.SetActive(true);
        _testImage.transform.localPosition = _localPos;
        _testImage.transform.localEulerAngles = _localRot;
    }
    public void DeActiveBlockArea()
    {
        _collider.enabled = false;

        _testImage.SetActive(false);
    }
}
