using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resoltuionDropdown;

    public GameObject gameobject;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions.Select(Resolution => new Resolution { width = Resolution.width, height = Resolution.height }).Distinct().ToArray(); //avoid duplication into the resoluion array
        resoltuionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resoltuionDropdown.AddOptions(options);
        resoltuionDropdown.value = currentResolutionIndex;
        resoltuionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

        gameobject.SetActive(false);

    }

    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMaster", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
