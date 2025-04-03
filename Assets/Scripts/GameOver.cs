using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for the GameOVer Scene Behaviour. 
/// <author> Tanner Parkes </author>
/// <references>
/// Referenced: (Make your MAIN MENU Quickly! | Unity UI Tutorial For Beginners) https://www.youtube.com/watch?v=DX7HyN7oJjE 
/// </summary>
public class GameOver : MonoBehaviour
{

    //Method for NewGame button to change the scene to the ingame scene.
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    //Method for the main menu button to change the scene to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Method for the Quit button to exit the application.
    public void QuitGame()
    {
        Application.Quit();
    }
}
