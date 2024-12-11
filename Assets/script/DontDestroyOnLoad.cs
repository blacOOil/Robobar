using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject settingPanel;
    private bool isPanelActive;
    
    void Awake() {
        DontDestroyOnLoad(settingPanel);
        isPanelActive = settingPanel.activeSelf;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPanelActive = !isPanelActive;
            settingPanel.SetActive(isPanelActive);
        }
    }
}
