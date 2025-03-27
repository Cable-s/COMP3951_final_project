using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for controlling in game PauseMenu behaviour.
/// Created by Tanner Parkes
/// Referenced: (PAUSE MENU in Unity) https://www.youtube.com/watch?v=JivuXdrIHK0
/// </summary>
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    /// <summary>
    /// Pause Method, brings up the PauseMenuUI.
    /// </summary>
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    /// <summary>
    /// Resume Method, hides the PauseMenu and resumes game.
    /// </summary>
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    /// <summary>
    /// MainMenu Method, returns the user to the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    /// <summary>
    /// Quit method, allows the user to exit the application from in the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
