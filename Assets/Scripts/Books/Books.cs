using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Books : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Button _bookButton;
    [SerializeField] private TMPro.TMP_Text _textMeshPro;

    public Action isClicked;

    private void OnEnable()
    {
        _bookButton.onClick.AddListener(HandleClicked);
    }

    private void OnDisable()
    {
        _bookButton.onClick.RemoveListener(HandleClicked);
    }

    private void Awake()
    {
        _textMeshPro.text = _name;
    }

    [ContextMenu("Click")]
    private void HandleClicked()
    {
        isClicked?.Invoke();
        Debug.Log("clicked");
    }
}