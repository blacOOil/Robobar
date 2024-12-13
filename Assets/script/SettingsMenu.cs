using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private float musicVolume = 0;

    // Initialize settings on start
    private void Start()
    {
        // Set the slider's value based on the current volume in the AudioMixer
        if (audioMixer.GetFloat("Volume", out float currentVolume))
        {
            musicVolume = currentVolume;
            volumeSlider.value = musicVolume;
        }

        // Add listener to slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Method to set volume
    public void SetVolume(float volume)
    {
        musicVolume = volume;
        audioMixer.SetFloat("Volume", volume);
    }

    // Method to toggle fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
