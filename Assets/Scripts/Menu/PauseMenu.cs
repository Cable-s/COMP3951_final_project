using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for controlling in game PauseMenu behaviour.
/// <author> Tanner Parkes </author>
/// <references>
/// Referenced: (PAUSE MENU in Unity) https://www.youtube.com/watch?v=JivuXdrIHK0
/// </references>
/// </summary>
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    /// <summary>
    /// Method that allows using the escape key to open the in-game menu
    /// </summary>
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
    /// Toggle pause method, added to toggle the pause menu when the menu button is clicked.
    /// </summary>
    public void togglePause()
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

    /// <summary>
    /// MainMenu Method, returns the user to the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    /// <summary>
    /// Quit method, allows the user to exit the application from in the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
