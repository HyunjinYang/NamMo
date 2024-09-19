using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrologueScene : BaseScene
{
    [Header("Characters")]
    [SerializeField] private GameObject _nammo;
    [SerializeField] private GameObject _heroNammo;
    [SerializeField] private GameObject _joonjeong;
    [SerializeField] private GameObject _hwarang;
    [SerializeField] private GameObject _hwarang_AI;
    [SerializeField] private GameObject _teacher;
    [SerializeField] private GameObject _liege;
    [SerializeField] private GameObject _king;
    [SerializeField] private GameObject _buddhistMonk;

    [SerializeField] private TutorialNPC _tutorialNPC;
    [Header("Scenes")]
    [SerializeField] private GameObject _scene1_1;
    [SerializeField] private GameObject _scene1_2;
    [SerializeField] private GameObject _scene1_3;
    [SerializeField] private GameObject _scene1_4;
    [SerializeField] private GameObject _trainingGround;

    UI_Conversation _conversationUI;
    UI_Tutorial _tutorialUI;
    UI_SpeechBubble _speechBubbleUI;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.PrologueScene;

        _heroNammo.SetActive(false);

        _speechBubbleUI = Managers.UI.MakeWorldSpaceUI<UI_SpeechBubble>();
        _speechBubbleUI.Init();
        _speechBubbleUI.DeActivateUI();

        UI_PrologueFadeInMessage prologueFadeIn = Managers.UI.ShowUI<UI_PrologueFadeInMessage>();
        prologueFadeIn.Init();
        prologueFadeIn.OnFadeInScreenEnd += StartPrologue;
        prologueFadeIn.ShowPrologueMessages();

        //StartPrologue();

        //Tutorial_Move();
    }
    private void StartPrologue()
    {
        _conversationUI = Managers.UI.ShowUI<UI_Conversation>();
        _conversationUI.RegisterFlowAction(0, MoveToJoonjeong);
        _conversationUI.RegisterFlowAction(8, MoveToScene1Left);
        _conversationUI.RegisterFlowAction(18, Tutorial_Move);
        _conversationUI.RegisterFlowAction(22, AppearKing);
        _conversationUI.RegisterFlowAction(31, FadeOutTemple);
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
        _nammo.transform.DOMoveX(GetSceneLeftEndPos(_scene1_1).x, 3.0f).SetEase(Ease.Linear);
        _joonjeong.transform.DOMoveX(GetSceneLeftEndPos(_scene1_1).x + 1, 3.0f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                FocusCamera(_scene1_2);
                MoveToTrainingGround();
            });
    }
    private void MoveToTrainingGround()
    {
        _nammo.transform.position = GetSceneLeftEndPos(_scene1_2);
        _joonjeong.transform.position = GetSceneLeftEndPos(_scene1_2) + Vector3.left;
        _nammo.GetComponent<SpriteRenderer>().flipX = true;
        _nammo.transform.DOMoveX(GetSceneLeftEndPos(_scene1_2).x + 10f, 1.5f).SetEase(Ease.Linear);
        _joonjeong.transform.DOMoveX(GetSceneLeftEndPos(_scene1_2).x + 9f, 1.5f).SetEase(Ease.Linear).OnComplete(_conversationUI.ShowNextInfos);
    }
    private void Tutorial_Move()
    {
        _joonjeong.transform.position = GetSceneLeftEndPos(_trainingGround) + Vector3.right * 10f;
        _speechBubbleUI.ActivateUI();
        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 0);

        if (_conversationUI)
        {
            _conversationUI.HideElements();
        }
        FocusCamera(_trainingGround);
        Camera.main.DOOrthoSize(12, 1);
        //Camera.main.gameObject.GetComponent<CameraController>().CameraMode = Define.CameraMode.FollowTarget;
        FocusCamera(_trainingGround);
        _heroNammo.SetActive(true);

        _tutorialUI = Managers.UI.ShowUI<UI_Tutorial>();
        _tutorialUI.Init();
        _tutorialUI.SetTutorialText(Define.TutorialType.Move);

        Managers.Scene.CurrentScene.Player.GetPlayerMovement().OnWalk += CheckMoveTutorial;
    }
    
    bool _moveLeft = false;
    bool _moveRight = false;
    private void CheckMoveTutorial(float value)
    {
        if (value == 0) return;
        if (value < 0) _moveLeft = true;
        else if (value > 0) _moveRight = true;
        if(_moveLeft && _moveRight)
        {
            Managers.Scene.CurrentScene.Player.GetPlayerMovement().OnWalk -= CheckMoveTutorial;
            Tutorial_Attack();
        }
    }
    private void Tutorial_Attack()
    {
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Attack);
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_AirAttack);
        _tutorialUI.SetTutorialText(Define.TutorialType.Attack);
        _tutorialNPC.OnDamaged += CheckAttackTutorial;

        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 1);
    }
    private void CheckAttackTutorial()
    {
        _tutorialNPC.OnDamaged -= CheckAttackTutorial;
        Tutorial_Parrying();
    }
    private void Tutorial_Parrying()
    {
        _tutorialNPC.CurrentPhase = NPCPhase.CloseAttack;
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Block);
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Parrying);
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Hurt);
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Invincible);
        _tutorialUI.SetTutorialText(Define.TutorialType.Parrying);

        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 2);

        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Parrying).OnAbilityActivated += CheckParryingTutorial;
    }
    private void CheckParryingTutorial()
    {
        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Parrying).OnAbilityActivated -= CheckParryingTutorial;
        Tutorial_Jump();
    }
    private void Tutorial_Jump()
    {
        _tutorialNPC.CurrentPhase = NPCPhase.RangeAttack;
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Jump);
        _tutorialUI.SetTutorialText(Define.TutorialType.Jump);
        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 3);
        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Jump).OnAbilityActivated += CheckJumpTutorial;
    }
    private void CheckJumpTutorial()
    {
        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Jump).OnAbilityActivated -= CheckJumpTutorial;
        Tutorial_Dash();
    }
    private void Tutorial_Dash()
    {
        Managers.Scene.CurrentScene.Player.GetASC().UnlockAbility(Define.GameplayAbility.GA_Dash);
        _tutorialUI.SetTutorialText(Define.TutorialType.Dash);
        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 4);
        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Dash).OnAbilityActivated += CheckDashTutorial;
    }
    private void CheckDashTutorial()
    {
        Managers.Scene.CurrentScene.Player.GetASC().GetAbility(Define.GameplayAbility.GA_Dash).OnAbilityActivated -= CheckDashTutorial;
        Tutorial_FreeFight();
    }
    private void Tutorial_FreeFight()
    {
        _tutorialNPC.CurrentPhase = NPCPhase.FreeFight;
        _tutorialUI.SetTutorialText(Define.TutorialType.Fight);
        _tutorialNPC.OnHpZero += CheckFreeFightTutorial;
        _speechBubbleUI.SetPosAndText(_joonjeong.transform.position + Vector3.up * 3, 7);
    }
    private void CheckFreeFightTutorial()
    {
        _tutorialNPC.OnHpZero -= CheckFreeFightTutorial;
        Destroy(_tutorialUI.gameObject);
        Camera.main.gameObject.GetComponent<CameraController>().CameraMode = Define.CameraMode.None;
        FocusCamera(_trainingGround, 1f);
        Managers.Scene.CurrentScene.Player.BlockInput = true;
        _speechBubbleUI.SetPosAndText(_hwarang_AI.transform.position + Vector3.up * 3, 8);

        StartCoroutine(CoMoveToScene3());
    }
    private void AppearKing()
    {
        _conversationUI.HideElements();
        _king.transform.DOMoveX(GetSceneRightEndPos(_scene1_3).x - 10f, 3f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                _conversationUI.ShowNextInfos();
            });
    }
    private void FadeOutTemple()
    {
        _conversationUI.HideElements();
        UI_PrologueFadeInMessage prologueFadeIn = Managers.UI.ShowUI<UI_PrologueFadeInMessage>();
        prologueFadeIn.Init();
        prologueFadeIn.OnFadeOutScreenEnd += (() =>
        {
            _nammo.transform.position = GetSceneLeftEndPos(_scene1_4) + Vector3.right * 10f;
            _joonjeong.transform.position = GetSceneLeftEndPos(_scene1_4) + Vector3.right * 8f;
            _buddhistMonk.transform.position = GetSceneRightEndPos(_scene1_4) - Vector3.right * 8f;
            FocusCamera(_scene1_4);
            prologueFadeIn.FadeInScreen();
        });
        prologueFadeIn.OnFadeInScreenEnd += FadeInTemple;
        prologueFadeIn.FadeOutScreen();
    }
    private void FadeInTemple()
    {
        _conversationUI.ShowNextInfos();
    }
    private Vector3 GetSceneLeftEndPos(GameObject scene)
    {
        return scene.transform.position + Vector3.left * 25 + Vector3.down * 2.8f;
    }
    private Vector3 GetSceneRightEndPos(GameObject scene)
    {
        return scene.transform.position + Vector3.right * 25 + Vector3.down * 2.8f;
    }
    private void FocusCamera(GameObject scene, float duration = 0)
    {
        Camera.main.transform.DOMove(new Vector3(scene.transform.position.x, scene.transform.position.y, -10), duration);
    }
    public override void Clear()
    {
    }
    IEnumerator CoMoveToScene3()
    {
        yield return new WaitForSeconds(3f);
        
        UI_PrologueFadeInMessage prologueFadeIn = Managers.UI.ShowUI<UI_PrologueFadeInMessage>();
        prologueFadeIn.Init();
        prologueFadeIn.OnFadeOutScreenEnd += (() =>
        {
            _nammo.transform.position = GetSceneLeftEndPos(_scene1_3) + Vector3.right * 10f;
            _joonjeong.transform.position = GetSceneLeftEndPos(_scene1_3) + Vector3.right * 8f;
            FocusCamera(_scene1_3);
            prologueFadeIn.FadeInScreen();
        });
        prologueFadeIn.OnFadeInScreenEnd += (() => _conversationUI.ShowNextInfos());
        prologueFadeIn.FadeOutScreen();
    }
}
