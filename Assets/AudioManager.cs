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
    [SerializeField] private GameObject settingsPanelPrefab; // The Settings Panel Prefab
    [SerializeField] private GameObject settingsCanvas; // The Canvas to spawn the panel into
    private GameObject currentSettingsPanel; // To keep currently instantiated panel

    private const string VOLUME_PARAMETER_NAME = "Volume"; // Name of the volume parameter in AudioMixer
    
    public static AudioManager instance;

    private float currentVolume = 1.0f; // Default volume
    public delegate void VolumeChanged(float newVolume);
    public event VolumeChanged onVolumeChanged;

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

        // Detect when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the settings panel visibility
            if (currentSettingsPanel == null)
            {
                // Settings panel not active, instantiate and show it
                ToggleSettingsPanel(true);
            }
            else
            {
                // Settings panel is active, hide it
                ToggleSettingsPanel(false);
            }
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
            audioSource.pitch = 1.2f;
            audioSource.Play();
            hasSwitchedToIntenseSound = true;
        }
    }

    public void ToggleSettingsPanel(bool show)
    {
        // If settingsCanvas is not assigned, try to find it dynamically
        if (settingsCanvas == null)
        {
            settingsCanvas = GameObject.Find("MainCanvas");

            // Check again if it's still null
            if (settingsCanvas == null)
            {
                Debug.LogWarning("MainCanvas could not be found! Make sure a GameObject named 'MainCanvas' exists.");
                return;
            }
        }

        // Use existing settings panel reference instead of instantiating
        if (settingsPanelPrefab != null)
        {
            settingsPanelPrefab.SetActive(show);
            Debug.Log($"Settings Panel is now {(show ? "visible" : "hidden")}.");
        }
        else
        {
            Debug.LogWarning("Settings Panel Prefab is not assigned in the inspector!");
        }

        if (show)
        {
            if (currentSettingsPanel == null)
            {
                currentSettingsPanel = Instantiate(settingsPanelPrefab, settingsCanvas.transform);
                currentSettingsPanel.transform.localPosition = Vector3.zero;  // Center the panel
                currentSettingsPanel.SetActive(true);

                // Flip the panel (mirror effect)
                Vector3 flippedScale = currentSettingsPanel.transform.localScale;
                flippedScale.x = -flippedScale.x;  // Negate the X axis to flip horizontally
                currentSettingsPanel.transform.localScale = flippedScale;
                Debug.Log("Settings Panel instantiated successfully.");
            }
        }
        else
        {
            if (currentSettingsPanel != null)
            {
                Destroy(currentSettingsPanel);  // Destroy the settings panel GameObject

                currentSettingsPanel = null;  // Remove reference to the panel when it's hidden
                Debug.Log("Settings Panel hidden.");
            }
        }
    }

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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); // Convert to decibels
        currentVolume = volume;

        // Notify all listeners about the volume change
        onVolumeChanged?.Invoke(volume);
    }

    public float GetVolume()
    {
        return currentVolume;  // Return the current volume
    }

}
