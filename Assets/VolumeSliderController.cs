using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    private Slider volumeSlider;

    private void Start()
    {
        // Find the Slider component on the same GameObject
        volumeSlider = GetComponent<Slider>();

        if (volumeSlider == null)
        {
            Debug.LogError("Slider component not found! Attach this script to a GameObject with a Slider.");
            return;
        }

        // Ensure AudioManager exists
        if (AudioManager.instance == null)
        {
            Debug.LogError("AudioManager instance not found! Ensure AudioManager is loaded in the scene.");
            return;
        }

        // Initialize slider value to the current volume
        volumeSlider.value = AudioManager.instance.GetVolume();

        // Add listener to sync AudioManager volume when slider value changes
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.instance.SetVolume(value);
            Debug.Log("Volume updated: " + value);
        });

        // Register a callback to update slider when volume changes
        AudioManager.instance.onVolumeChanged += UpdateSliderValue;
    }

    private void UpdateSliderValue(float newValue)
    {
        // Update the slider value without triggering its onValueChanged listener
        if (volumeSlider != null && volumeSlider.value != newValue)
        {
            volumeSlider.value = newValue;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (AudioManager.instance != null)
        {
            AudioManager.instance.onVolumeChanged -= UpdateSliderValue;
        }
    }
}
