using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    private Toggle fullscreenToggle;

    private void Start()
    {
        // Get the Toggle component
        fullscreenToggle = GetComponent<Toggle>();

        if (fullscreenToggle == null)
        {
            Debug.LogError("Toggle component not found on FullscreenController!");
            return;
        }

        // Load fullscreen state from PlayerPrefs
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1; // Default to true

        // Ensure fullscreen state is set first
        Screen.fullScreen = isFullscreen;

        // Set the toggle state based on the fullscreen preference
        fullscreenToggle.isOn = isFullscreen;

        // Add listener to toggle for changes
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        Debug.Log("Toggle component found and initialized.");
    }

    private void Update()
    {
        Debug.Log("update?");

        // Check if the current fullscreen state is different from the toggle's state
        if (fullscreenToggle.isOn != Screen.fullScreen)
        {
            Debug.Log($"Syncing: Toggle state ({fullscreenToggle.isOn}) doesn't match Screen.fullScreen ({Screen.fullScreen})");
            fullscreenToggle.isOn = Screen.fullScreen; // Sync the toggle to match the actual fullscreen state
        }
    }

    private void SetFullscreen(bool isFullscreen)
    {
        // Apply fullscreen to the screen
        Screen.fullScreen = isFullscreen;

        fullscreenToggle.isOn = isFullscreen;

        // Save fullscreen preference
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        // Debug log to verify the change
        Debug.Log("Fullscreen mode set to: " + isFullscreen);
    }
}
