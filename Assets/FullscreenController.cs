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

        // Initialize toggle based on current fullscreen state
        fullscreenToggle.isOn = Screen.fullScreen;

        // Add listener for toggle changes
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen mode set to: " + isFullscreen);
    }
}
