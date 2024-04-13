using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
