using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject settingPanel;
    public static DontDestroyOnLoad instance;
    
    void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingPanel != null)
            {
                // Toggle the active state of the settings panel
                bool isActive = settingPanel.activeSelf;
                settingPanel.SetActive(!isActive);
            }
            else
            {
                Debug.LogWarning("Setting panel is not assigned in the Inspector!");
            }
        }
    }
}
