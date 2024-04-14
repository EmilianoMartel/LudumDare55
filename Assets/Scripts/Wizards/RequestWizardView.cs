using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RequestWizardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;

    [SerializeField] private List<string> _gretingMessage = new List<string>();
    [SerializeField] private List<string> _requestMessage = new List<string>();
    [SerializeField] private List<string> _endMessage = new List<string>();

    private string _message = "";

    public void ReceiveDungeonData(string wizardName, string dungeonName)
    {
        int indexGrettings = UnityEngine.Random.Range(0, _gretingMessage.Count);
        int indexRequest = UnityEngine.Random.Range(0, _requestMessage.Count);
        int indexEnd = UnityEngine.Random.Range(0, _endMessage.Count);
        _message = string.Format($"{_gretingMessage[indexGrettings]} {wizardName}\n{_requestMessage[indexRequest]} {dungeonName} \n{_endMessage[indexEnd]}.");
        _messageText.text = _message;
    }
}
