using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseInteractable : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected Model_InteractionTextData _originInteractionTextData;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected Vector2 _uiOffset;

    protected Model_InteractionTextData _currentInteractionTextData;
    protected UI_Interaction _interactionUI = null;

    protected PlayerController _player = null;

    private bool _subscribedInput = false;
    void Start()
    {
        Init();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null) return;
        _player = collision.gameObject.GetComponent<PlayerController>();
        HandleTriggerEnterEvent();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null) return;
        _player = null;
        HandleTriggerExitEvent();
    }
    protected virtual void Init()
    {
        _collider = GetComponent<Collider2D>();
        _currentInteractionTextData = _originInteractionTextData;
    }
    protected virtual void HandleTriggerEnterEvent()
    {
        _interactionUI = Managers.UI.ShowUI<UI_Interaction>();
        _interactionUI.Init();
        _interactionUI.SetInteractionText(_currentInteractionTextData);
        _interactionUI.transform.position = transform.position + new Vector3(_uiOffset.x, _uiOffset.y, 0);

        Managers.Scene.CurrentScene.Player.OnInteractionInputPerformed += HandleInteractionEvent;
        _subscribedInput = true;
    }
    protected virtual void HandleTriggerExitEvent()
    {
        CloseInteractionUIAndCutOffAction();
    }
    protected virtual void HandleInteractionEvent()
    {
        CloseInteractionUIAndCutOffAction();
    }
    private void CloseInteractionUIAndCutOffAction()
    {
        if (_subscribedInput == false) return;
        _subscribedInput = false;
        if (Managers.Scene.CurrentScene == null) return;
        if (Managers.Scene.CurrentScene.Player.OnInteractionInputPerformed != null)
        {
            Managers.Scene.CurrentScene.Player.OnInteractionInputPerformed -= HandleInteractionEvent;
        }
        if (_interactionUI)
        {
            _interactionUI.Close();
        }
    }
}
