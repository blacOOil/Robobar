using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public GameObject settingPanelInGameScene;

    private float musicVolume = 0;

    // Initialize settings on start
    private void Start()
    {
        if (settingPanelInGameScene != null) {
            settingPanelInGameScene.SetActive(true);
        }
        

        float currentVolume;
        
        // Set the slider's value based on the current volume in the AudioMixer
        if (audioMixer.GetFloat("Volume", out currentVolume))
        {
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }

        // Add listener to slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Method to set volume
    public void SetVolume(float sliderValue)
    {
        // Convert slider value (0–1) to decibels (-80 to 0 dB)
        float volumeInDecibels = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;

        // Set the volume on the AudioMixer
        audioMixer.SetFloat("Volume", volumeInDecibels);

        // Debugging for verification
        Debug.Log($"Slider Value: {sliderValue}, Volume in dB: {volumeInDecibels}");
    }

    // Method to toggle fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // Save fullscreen preference
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Fullscreen mode set to: " + isFullscreen);
    }

    public void WhenPressBackInGameScene() {
        settingPanelInGameScene.SetActive(false);
    }
}
