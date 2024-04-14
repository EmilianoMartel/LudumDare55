using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _optionsMenu;

    [Header("Scenes to load")]
    [SerializeField] private string _startGame = "add  gameplay level name here";

    [Header("Event system first selectec button")]
    [SerializeField] private GameObject _creditsMenuButtonSelected;
    [SerializeField] private GameObject _mainMenuButtonSelected;
    [SerializeField] private GameObject _optionMenuButtonSelected;

    public void PlayButton()
    {
        SceneManager.LoadScene(_startGame);
    }

    public void CreditsButton()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_creditsMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void OptionsButton()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_optionMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void BackToMenu()
    {
        _mainMenu.SetActive(true);
        _creditsMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_mainMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
