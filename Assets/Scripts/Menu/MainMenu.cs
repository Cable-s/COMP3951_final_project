using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for the MainMenu Behaviour. 
/// <author> Tanner Parkes </author>
/// <references>
/// Referenced: (Make your MAIN MENU Quickly! | Unity UI Tutorial For Beginners) https://www.youtube.com/watch?v=DX7HyN7oJjE
/// </references> 
/// </summary>
public class MainMenu : MonoBehaviour
{

    //Method for Start Button to change the scene to the ingame scene.
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Method for the Quit button to exit the application.
    public void QuitGame()
    {
        Application.Quit();
    }
}
