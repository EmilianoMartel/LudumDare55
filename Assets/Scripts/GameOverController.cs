using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private IsWinOrLoseData _data;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _winCanvas;

    [SerializeField] private GameObject FirstButtonSelected;

    [Header("Sound")]
    [SerializeField] private AudioSource _winMenuSound;
    [SerializeField] private AudioSource _loseMenuSound;

    [SerializeField] private string buttonToMenu = "add level name here";
    [SerializeField] private string buttonToResetGame = "add level name here";

    private void Awake()
    {
        GameOverLogic(_data.GetBool());
    }

    private void GameOverLogic(bool isWining)
    {
        if (isWining)
        {
            _winCanvas.SetActive(true);
            _loseCanvas.SetActive(false);
            _winMenuSound.Play();
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(FirstButtonSelected, new BaseEventData(eventSystem));
        }
        else
        {
            _winCanvas.SetActive(false);
            _loseCanvas.SetActive(true);
            _loseMenuSound.Play();
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(FirstButtonSelected, new BaseEventData(eventSystem));
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(buttonToResetGame);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(buttonToMenu);
    }
}