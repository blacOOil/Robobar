using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioCode audioCode;

    void Start() {
        if (audioCode != null) {
            audioCode.MainMenuSound();
        }
    }
}
