using UnityEngine;

/// <summary>
/// Manages the mute state of music across scenes
/// Author: Jason Peacock
/// </summary>
public class AudioScipt : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private static bool hasInitialized = false;
    public void Start()
    {
        //Only initialize PlayerPrefs once on startup, since hasInitialized is static works across scenes
        if (!hasInitialized)
        {
            hasInitialized = true;
            InitializePrefsOnlyOnStartup();
        }

        int muteState = PlayerPrefs.GetInt("MuteState", 0);
        bool muteStateBool;

        //Convert int value to bool (Cannot store bool in PlayerPrefs)
        switch (muteState)
        {
            case 0: muteStateBool = false; break;
            case 1: muteStateBool = true; break;
            default: muteStateBool = false; break;
        }

        audioSource.mute = muteStateBool;
    }

    /// <summary>
    /// Inverts the current value of mute
    /// https://docs.unity3d.com/ScriptReference/AudioSource-mute.html
    /// </summary>
    public void ToggleMute()
    {
        int value = !audioSource.mute ? 1 : 0;
        PlayerPrefs.SetInt("MuteState", value);
        PlayerPrefs.Save();
        audioSource.mute = !audioSource.mute;
    }

    /// <summary>
    /// Sets initial state of mute to false on startup
    /// </summary>
    private void InitializePrefsOnlyOnStartup()
    {
        PlayerPrefs.SetInt("MuteState", 0);
        PlayerPrefs.Save();
    }
}
