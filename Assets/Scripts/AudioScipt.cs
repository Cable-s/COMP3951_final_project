using UnityEngine;

public class AudioScipt : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private static bool hasInitialized = false;
    public void Start()
    {
        if (!hasInitialized)
        {
            hasInitialized = true;
            InitializePrefsOnlyOnStartup();
        }

        int muteState = PlayerPrefs.GetInt("MuteState", 0);

        bool muteStateBool;

        switch (muteState)
        {
            case 0: muteStateBool = false; break;
            case 1: muteStateBool = true; break;
            default: muteStateBool = false; break;
        }

        audioSource.mute = muteStateBool;
    }

    public void ToggleMute()
    {
        int value = !audioSource.mute ? 1 : 0;
        PlayerPrefs.SetInt("MuteState", value);
        PlayerPrefs.Save();
        audioSource.mute = !audioSource.mute;
    }

    private void InitializePrefsOnlyOnStartup()
    {
        PlayerPrefs.SetInt("MuteState", 0);
        PlayerPrefs.Save();
    }
}
