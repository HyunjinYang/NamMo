using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueScene : BaseScene
{
    [Header("Characters")]
    [SerializeField] private GameObject _nammo;
    [SerializeField] private GameObject _heroNammo;
    [SerializeField] private GameObject _joonjeong;
    [SerializeField] private GameObject _hwarang;
    [SerializeField] private GameObject _teacher;
    [SerializeField] private GameObject _liege;
    [SerializeField] private GameObject _king;
    [SerializeField] private GameObject _buddhistMonk;
    [Header("Scenes")]
    [SerializeField] private GameObject _scene1;
    [SerializeField] private GameObject _scene2;
    [SerializeField] private GameObject _scene3;
    [SerializeField] private GameObject _scene4;
    [SerializeField] private GameObject _trainingGround;

    UI_Conversation _conversationUI;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.PrologueScene;

        _heroNammo.SetActive(false);

        UI_PrologueFadeInMessage prologueFadeIn = Managers.UI.ShowUI<UI_PrologueFadeInMessage>();
        prologueFadeIn.OnPrologueFadeInEnd += StartPrologue;

        //StartPrologue();
    }
    private void StartPrologue()
    {
        _conversationUI = Managers.UI.ShowUI<UI_Conversation>();
        _conversationUI.RegisterFlowAction(0, MoveToJoonjeong);
        _conversationUI.RegisterFlowAction(8, MoveToScene1Left);
        _conversationUI.RegisterFlowAction(18, Tutorial_Move);
    }
    private void MoveToJoonjeong()
    {
        _conversationUI.HideElements();
        _nammo.transform.DOMoveX(_joonjeong.transform.position.x - 1, 3.0f).SetEase(Ease.Linear).OnComplete(_conversationUI.ShowNextInfos);
    }
    private void MoveToScene1Left()
    {
        _conversationUI.HideElements();
        _nammo.GetComponent<SpriteRenderer>().flipX = false;
        _nammo.transform.DOMoveX(GetSceneLeftEndPos(_scene1).x, 3.0f).SetEase(Ease.Linear);
        _joonjeong.transform.DOMoveX(GetSceneLeftEndPos(_scene1).x + 1, 3.0f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                FocusCamera(_scene2);
                MoveToTrainingGround();
            });
    }
    private void MoveToTrainingGround()
    {
        _nammo.transform.position = GetSceneLeftEndPos(_scene2);
        _joonjeong.transform.position = GetSceneLeftEndPos(_scene2) + Vector3.left;
        _nammo.GetComponent<SpriteRenderer>().flipX = true;
        _nammo.transform.DOMoveX(GetSceneLeftEndPos(_scene2).x + 4f, 1.5f).SetEase(Ease.Linear);
        _joonjeong.transform.DOMoveX(GetSceneLeftEndPos(_scene2).x + 3f, 1.5f).SetEase(Ease.Linear).OnComplete(_conversationUI.ShowNextInfos);
    }
    private void Tutorial_Move()
    {
        _conversationUI.HideElements();
        FocusCamera(_trainingGround);
        Camera.main.DOOrthoSize(10, 1);
        Camera.main.gameObject.GetComponent<CameraController>().CameraMode = Define.CameraMode.FollowTarget;
        _heroNammo.SetActive(true);
    }
    private Vector3 GetSceneLeftEndPos(GameObject scene)
    {
        return scene.transform.position + Vector3.left * 10 + Vector3.down / 2;
    }
    private Vector3 GetSceneRightEndPos(GameObject scene)
    {
        return scene.transform.position + Vector3.right * 10 + Vector3.down / 2;
    }
    private void FocusCamera(GameObject scene)
    {
        Camera.main.transform.position = new Vector3(scene.transform.position.x, scene.transform.position.y, -10);
    }
    public override void Clear()
    {
    }
}
