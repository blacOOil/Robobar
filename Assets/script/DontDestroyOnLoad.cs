using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle settings panel using AudioManager
            if (AudioManager.instance != null)
            {
                AudioManager.instance.ToggleSettingsPanel();
            }
        }
    }
}
