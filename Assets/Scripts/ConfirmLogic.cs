using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmLogic : MonoBehaviour
{
    [SerializeField] private SendDemonButton _sendDemon;
    [SerializeField] private TimerRequest _timerRequest;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    public Action confirmEvent = delegate { };

    private void OnEnable()
    {
        _sendDemon.sendDemonEvent += HandleSendDemon;
        _confirmButton.onClick.AddListener(HandleClickConfirm);
        _cancelButton.onClick.AddListener(HandleCancel);
    }

    private void OnDisable()
    {
        _sendDemon.sendDemonEvent -= HandleSendDemon;
        _confirmButton.onClick?.RemoveListener(HandleClickConfirm);
        _cancelButton.onClick.RemoveListener(HandleCancel);
    }

    private void Awake()
    {
        _canvas.enabled = false;
    }

    private void HandleSendDemon()
    {
        _canvas.enabled = true;
    }

    private void HandleClickConfirm()
    {
        confirmEvent?.Invoke();
        _canvas.enabled = false;
    }

    private void HandleCancel()
    {
        _canvas.enabled = false;
    }
}
