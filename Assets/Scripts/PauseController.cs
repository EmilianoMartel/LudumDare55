using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Others Settings")]
    //[SerializeField] private AudioSource levelMusic;
    [SerializeField] private GameObject pauseMenu;

    [Header("Buttons Settings")]
    [SerializeField] private string buttonToMenu;
    [SerializeField] private GameObject eventSystemButtonSelected;
    //[SerializeField] private GameObject eventSystemSecondButtonSelected;

    void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }
    }

    /// <summary>
    /// This method activates the menu, pauses the game music and freezes the time.
    /// </summary>
    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        //levelMusic.Pause();
        Time.timeScale = 0f;
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(eventSystemButtonSelected, new BaseEventData(eventSystem));
    }

    /// <summary>
    /// This method deactivates the menu, returning to the game at the moment before pausing it.
    /// </summary>
    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        //levelMusic.Play();
        Time.timeScale = 1f;
        //var eventSystem = EventSystem.current;
        //eventSystem.SetSelectedGameObject(eventSystemSecondButtonSelected, new BaseEventData(eventSystem));
    }

    /// <summary>
    /// This method loads the main menu scene.
    /// </summary>
    public void MenuButton()
    {
        SceneManager.LoadScene(buttonToMenu);
        Time.timeScale = 1f;
    }
}
