using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _tutorial;

    [Header("Sound")]
    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource optionsMenuSound;
    [SerializeField] private AudioSource creditsMenuSound;


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
        _tutorial.SetActive(false);
        mainMenuSound.Stop();
        creditsMenuSound.Play();
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_creditsMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void OptionsButton()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
        _tutorial.SetActive(false);
        optionsMenuSound.Play();
        mainMenuSound.Stop();
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_optionMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void BackToMenu()
    {
        _mainMenu.SetActive(true);
        mainMenuSound.Play();
        creditsMenuSound.Stop();
        optionsMenuSound.Stop();
        _creditsMenu.SetActive(false);
        _tutorial.SetActive(false);
        _optionsMenu.SetActive(false);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(_mainMenuButtonSelected, new BaseEventData(eventSystem));
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        SceneManager.LoadScene("PauseMenu");
    }

    public void Tutorial()
    {
        _tutorial.SetActive(true);
        _creditsMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(false);
    }
}
