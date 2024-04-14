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
    [SerializeField] private Color _color;

    private string _message = "";

    public void ReceiveDungeonData(string wizardName, string dungeonName)
    {
        _message = string.Format($"{GetRandomMessage(_gretingMessage)} {ColorText(wizardName, _color)}\n" +
            $"{GetRandomMessage(_requestMessage)} {ColorText(dungeonName, _color)} \n" +
            $"{GetRandomMessage(_endMessage)}.");
        _messageText.text = _message;
    }

    private string GetRandomMessage(List<string> message)
    {
        int index = UnityEngine.Random.Range(0, message.Count);
        return message[index];
    }

    public void ReciveNoneData()
    {
        _messageText.text = "Waiting for new request";
    }

    public void WaitForEndDay()
    {
        _messageText.text = "Waiting for demons to close the office";
    }

    private string ColorText(string text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
    }
}