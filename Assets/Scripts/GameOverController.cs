using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private ActionChannel<bool> isWinGameEvent;

    [SerializeField] private GameObject gameOverMenu;

    //[SerializeField] private HealthPoints playerHealthPoints;

    [SerializeField] private GameObject FirstButtonSelected;


    [SerializeField] private string buttonToMenu = "add level name here";
    [SerializeField] private string buttonToResetGame = "add level name here";

    //private void OnEnable()
    //{
    //    isWinGameEvent.Subscription(OnEventLose);

    //}

    //private void OnDisable()
    //{
    //    isWinGameEvent -= OnEventLose;
    //}

    public void OnEventLose()
    {
        //if (playerHealthPoints.health <= 0)
        //{
        //    gameOverMenu.SetActive(true);
        //    var eventSystem = EventSystem.current;
        //    eventSystem.SetSelectedGameObject(FirstButtonSelected, new BaseEventData(eventSystem));
        //}
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
