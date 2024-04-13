using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeamonButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private DemonCreatorController _creator;

    private Sprite _face;
    private Demon _demon;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleSendDemon);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleSendDemon);
    }

    public void ReciveDemon(Demon demon, Sprite face)
    {
        _demon = demon;
        _face = face;
        _button.image.sprite = _face;
    }

    private void HandleSendDemon()
    {
        _creator.sendDeamonAction?.Invoke(_demon,_face);
    }
}