using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLogic : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;
    [SerializeField] private string _gameScene = "GameplayScene";
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _playButton;

    private int _index = 0;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(HandleNextTutorial);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(HandleNextTutorial);
    }


    private void Awake()
    {
        _playButton.gameObject.SetActive(false);
    }

    private void HandleNextTutorial()
    {
        _index++;
        if (_index >= _sprites.Count-1)
        {
            _playButton.gameObject.SetActive(true);
            _nextButton.gameObject.SetActive(false);
        }
        _image.sprite = _sprites[_index];
    }
}
