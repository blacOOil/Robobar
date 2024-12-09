using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCode : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip cleaningAudio; //1
    [SerializeField] public AudioClip minigameAudio; //2
    [SerializeField] public AudioClip upgradingAudio; //3
    [SerializeField] public AudioClip resumeAudio; //4
    [SerializeField] public AudioClip normalSound;
    [SerializeField] public AudioClip intenseSound;
    private bool hasSwitchedToIntenseSound = false;
    public void PlayAudioForState(int gamestateNumber) {
        if (audioSource == null) return;

        audioSource.Stop(); // Stop any currently playing audio

        // Select and play the appropriate audio clip based on the game state
        AudioClip selectedClip = null;
        switch (gamestateNumber)
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
                return; // No audio for invalid states
        }

        if (selectedClip != null)
        {
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
    }

    public void PlayNormalSound() {
        if (audioSource != null && normalSound != null)
        {
            audioSource.clip = normalSound;
            audioSource.Play();
            hasSwitchedToIntenseSound = false;
        }
    }

    public void PlayIntenseSound() {
        if (audioSource != null && intenseSound != null)
        {
            audioSource.clip = intenseSound;
            audioSource.Play();
            hasSwitchedToIntenseSound = true;
        }
    }

    public void CheckAndSwitchAudio(float timePercentage, float value) {
        if (timePercentage < value && !hasSwitchedToIntenseSound)
        {
            PlayIntenseSound();
        }
        else if (timePercentage >= value && hasSwitchedToIntenseSound)
        {
            PlayNormalSound();
        }
    }
}
