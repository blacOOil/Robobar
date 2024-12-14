using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource; // Audio Source for playing clips
    public AudioMixer audioMixer; // Audio Mixer to control volume

    [Header("Game State Audio Clips")]
    public AudioClip mainMenuAudio;   // Audio for the main menu
    public AudioClip cleaningAudio;   // Game state 1
    public AudioClip minigameAudio;   // Game state 2
    public AudioClip upgradingAudio;  // Game state 3
    public AudioClip resumeAudio;     // Game state 4
    public AudioClip normalSound;     // Normal gameplay audio
    public AudioClip intenseSound;    // Intense gameplay audio

    private bool hasSwitchedToIntenseSound = false;

    [Header("Settings Panel Management")]
    [SerializeField] private GameObject settingsPanelPrefab;
    [SerializeField] private GameObject settingsCanvas;
    // public GameObject settingPanelPrefab;
    // private GameObject currentSettingPanel;
    
    public static AudioManager instance;

    private void Awake()
    {
        // Singleton Pattern to ensure one instance persists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Ensure an AudioSource exists
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PlayMainMenuAudio();
    }

    private void Update() {
        // Find the canvas dynamically if not assigned
        if (settingsCanvas == null)
        {
            settingsCanvas = GameObject.Find("MainCanvas");
        }

        // Instantiate Settings Panel when pressing ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    public void PlayAudioForState(int gameStateNumber)
    {
        if (audioSource == null) return;

        audioSource.Stop(); // Stop any currently playing audio

        AudioClip selectedClip = null;

        switch (gameStateNumber)
        {
            case 1:
                selectedClip = cleaningAudio;
                break;
            case 2:
                selectedClip = minigameAudio;
                break;
            case 3:
                selectedClip = upgradingAudio;
                break;
            case 4:
                selectedClip = resumeAudio;
                break;
            default:
                Debug.LogWarning("Invalid game state!");
                return;
        }

        if (selectedClip != null)
        {
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
    }

    public void PlayMainMenuSound()
    {
        if (audioSource != null && mainMenuAudio != null)
        {
            audioSource.clip = mainMenuAudio;
            audioSource.Play();
        }
    }

    public void CheckAndSwitchAudio(float timePercentage, float value)
    {
        if (timePercentage < value && !hasSwitchedToIntenseSound)
        {
            PlayIntenseSound();
        }
        else if (timePercentage >= value && hasSwitchedToIntenseSound)
        {
            PlayNormalSound();
        }
    }

    public void PlayNormalSound()
    {
        if (audioSource != null && normalSound != null)
        {
            audioSource.clip = normalSound;
            audioSource.Play();
            hasSwitchedToIntenseSound = false;
        }
    }

    public void PlayIntenseSound()
    {
        if (audioSource != null && intenseSound != null)
        {
            audioSource.clip = intenseSound;
            audioSource.Play();
            hasSwitchedToIntenseSound = true;
        }
    }

    public void ToggleSettingsPanel()
    {
        // if (currentSettingPanel == null)
        // {
        //     // Spawn settings panel
        //     if (settingPanelPrefab != null && settingCanvas != null)
        //     {
        //         currentSettingPanel = Instantiate(settingPanelPrefab, Vector3.zero, Quaternion.identity);
        //         currentSettingPanel.transform.SetParent(settingCanvas.transform, false);
        //         Debug.Log("Settings panel instantiated.");
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Settings Panel Prefab or Canvas is not assigned!");
        //     }
        // }
        // else
        // {
        //     // Destroy settings panel if it already exists
        //     Destroy(currentSettingPanel);
        //     currentSettingPanel = null;
        //     Debug.Log("Settings panel destroyed.");
        // }

        if (settingsPanelPrefab == null || settingsCanvas == null)
        {
            Debug.LogWarning("Settings Panel Prefab or Canvas is not assigned!");
            return; // Exit early to avoid further issues
        }

        // Instantiate the Settings Panel if everything is assigned
        GameObject newPanel = Instantiate(settingsPanelPrefab, settingsCanvas.transform);
        newPanel.transform.localPosition = Vector3.zero; // Center the panel
        Debug.Log("Settings Panel instantiated successfully.");
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // Ensure the panel prefab persists across scenes
    //     if (settingPanelInstance == null)
    //     {
    //         GameObject canvas = GameObject.Find("MainCanvas");
    //         if (canvas != null && settingPanelPrefab != null)
    //         {
    //             settingPanelInstance = Instantiate(settingPanelPrefab, canvas.transform);
    //             settingPanelInstance.SetActive(false);
    //         }
    //         else
    //         {
    //             Debug.LogWarning("MainCanvas or SettingPanelPrefab not found in the scene.");
    //         }
    //     }
    // }

    // Play main menu audio
    public void PlayMainMenuAudio()
    {
        if (mainMenuAudio != null)
        {
            audioSource.clip = mainMenuAudio;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // public void SetVolume(float volume)
    // {
    //     audioMixer.SetFloat("Volume", volume);
    // }

    // public void ToggleSettingsPanel()
    // {
    //     if (settingPanelInstance != null)
    //         settingPanelInstance.SetActive(!settingPanelInstance.activeSelf);
    // }

}
