using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendDemonButton : MonoBehaviour
{
    [SerializeField] private Button _sendDemon;

    public Action sendDemonEvent = delegate { };

    private void OnEnable()
    {
        _sendDemon.onClick.AddListener(HandleSendDemon);
    }

    private void OnDisable()
    {
        _sendDemon.onClick.RemoveListener(HandleSendDemon);
    }

    private void HandleSendDemon()
    {
        sendDemonEvent?.Invoke();
    }
}