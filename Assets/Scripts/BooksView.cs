using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BooksView : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _nextButton;
    [SerializeField] private UnityEngine.UI.Button _prevButton;
    [SerializeField] private UnityEngine.UI.Button _closeButton;
    [SerializeField] private Books _book;
    [SerializeField] private List<Sprite> _spriteList = new List<Sprite>();

    [Header("Visual parameters")]
    [SerializeField] private UnityEngine.UI.Image _panel;
    [SerializeField] private Canvas _panelCanvas;

    private int _index = 0;

    private void OnEnable()
    {
        _book.isClicked += HandleBookClick;
        _nextButton.onClick.AddListener(HandleNextButtonClick);
        _prevButton.onClick.AddListener(HandlePrevButton);
        _closeButton.onClick.AddListener(HandleCloseButton);
    }

    private void OnDisable()
    {
        _book.isClicked -= HandleBookClick;
        _nextButton.onClick.RemoveListener(HandleNextButtonClick);
        _prevButton.onClick.RemoveListener(HandlePrevButton);
        _closeButton.onClick.RemoveListener(HandleCloseButton);
    }

    private void Awake()
    {
        if (!_book)
        {
            Debug.LogError($"{name}: Book is nulls\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        _panelCanvas.enabled = false;
    }

    private void HandleNextButtonClick()
    {
        _index++;
        if (_index >= _spriteList.Count)
        {
            _index = _spriteList.Count - 1;
        }
        _panel.sprite = _spriteList[_index];
    }

    private void HandlePrevButton()
    {
        _index--;
        if (_index < 0)
        {
            _index = 0;
        }
        _panel.sprite = _spriteList[_index];
    }

    private void HandleCloseButton()
    {
        _panelCanvas.enabled = false;
    }

    private void HandleBookClick()
    {
        _index = 0;
        _panel.sprite = _spriteList[_index];
        _panelCanvas.enabled = true;
    }
}