using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resoltuionDropdown;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resoltuionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        resoltuionDropdown.AddOptions(options);
        
    }

    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMaster", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
