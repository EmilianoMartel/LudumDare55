using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Books : MonoBehaviour
{
    [SerializeField] private Button _bookButton;
    [SerializeField] private List<GameObject> _gameObject;
    [SerializeField] private Button _closeButton;

    public Action isClicked;

    private void OnEnable()
    {
        _bookButton.onClick.AddListener(HandleClicked);
        _closeButton.onClick.AddListener(HandleCloseClicked);
    }

    private void OnDisable()
    {
        _bookButton.onClick.RemoveListener(HandleClicked);
        _closeButton.onClick.AddListener(HandleCloseClicked);
    }

    [ContextMenu("Click")]
    private void HandleClicked()
    {
        isClicked?.Invoke();
        for (int i = 0; i < _gameObject.Count; i++)
        {
            if (_gameObject[i].activeSelf)
            {
                _gameObject[i].SetActive(false);
            }
        }
        Debug.Log("clicked");
    }

    private void HandleCloseClicked()
    {
        for (int i = 0; i < _gameObject.Count; i++)
        {
            if (!_gameObject[i].activeSelf)
            {
                _gameObject[i].SetActive(true);
            }
        }
    }
}