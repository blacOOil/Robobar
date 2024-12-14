using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Timemanager { 
public class TimerCode : MonoBehaviour {
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] public float remainingTime,startedRemainingtime;
    [SerializeField] public GameObject GameOverUI;
    public bool IstimeCounting;
    public Material material;
    private float timePercentage;

    [Header("Rotation Settings")]
    [SerializeField] public List<RotationScript> rotationScripts; //Ref form RotationScript
    [SerializeField] public float timerAllLessThan = 0.15f; //Percentage All Timer
    [SerializeField] public float minRotationSpeed; //Min Rotate Speed
    [SerializeField] public float maxRotationSpeed; //Max Rotate Speed
    [SerializeField] public float maximunRotationSpeed; //Max Rotate Speed

    [Header("Light Settings")]
    [SerializeField] public float Parameter1; //flashing Speed 0.5
    [SerializeField] public float Parameter2; //flashing Speed 0.2

    //[Header("Audio Settings")]
    //[SerializeField] private AudioCode audioCode;

    void Start() {
        startedRemainingtime = remainingTime;
        IstimeCounting = true;
        GameOverUI.SetActive(false);

        if (material != null) {
            material.EnableKeyword("_EMISSION");
            UpdateEmissionColor();
        }
        // elapsedTime += Time.deltaTime;
        //  TimeText.text = elapsedTime.ToString();

        foreach (var script in rotationScripts) {
            script.SetMinSpeed(minRotationSpeed);
            script.SetMaxSpeed(maxRotationSpeed);
            script.SetRotationSpeed(0f); // Start with no rotation
        }

        // if (audioCode != null) {
        //     audioCode.PlayNormalSound();
        // }

        StartCoroutine(SmoothlyUpdateRotationSpeeds());
    }

    // Update is called once per frame
    void Update() {
        if(IstimeCounting == true) {
            CountingDown();
            timePercentage = remainingTime / startedRemainingtime;

            UpdateRotationSpeed();
            UpdateEmissionColor();

            // if (audioCode != null) {
            //     audioCode.CheckAndSwitchAudio(timePercentage, timerAllLessThan);
            // }
        }

        if (remainingTime <= 0) {
            TimeText.text = string.Format("{0:00}:{1:00}", 0, 0);
            GameOverUI.SetActive(true);
            IstimeCounting = false;

            // Stop all rotate
            foreach (var script in rotationScripts) {
                script.SetRotationSpeed(0f);
            }
        }
      
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void resettimer() {
        IstimeCounting = false;
        remainingTime = startedRemainingtime;
        GameOverUI.SetActive(false);

        // Stop all rotate
        foreach (var script in rotationScripts) {
            script.SetRotationSpeed(0f);
        }
    }
    public void CountingDown() {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int second = Mathf.FloorToInt(remainingTime % 60);
            TimeText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }

    private void UpdateRotationSpeed() {
        if (timePercentage < timerAllLessThan) {
            foreach (var script in rotationScripts) {
                // Calculate target speed based on initial speed and time percentage
                float targetSpeed = Mathf.Lerp(script.RotationSpeed, maximunRotationSpeed, 1f - timePercentage);

                // Clamp the target speed to ensure it doesn't exceed the maximum rotation speed
                targetSpeed = Mathf.Clamp(targetSpeed, script.MinSpeed, maximunRotationSpeed);

                // Smoothly interpolate the current rotation speed toward the target speed
                float smoothedSpeed = Mathf.Lerp(script.RotationSpeed, targetSpeed, Time.deltaTime * 2f); // Adjust factor for slower/faster transitions

                // Apply the smoothed speed
                script.SetRotationSpeed(smoothedSpeed);
            }
        } else {
            // Keep the rotation speed at the initial speed if above the 70% threshold
            foreach (var script in rotationScripts) {
                float smoothedSpeed = Mathf.Lerp(script.RotationSpeed, script.MinSpeed, Time.deltaTime * 2f); // Smooth transition to initial speed
                script.SetRotationSpeed(smoothedSpeed);
            }
        }

    }

    private void UpdateEmissionColor() {
        // Determine the color based on the time percentage
        Color emissionColor;

        if (timePercentage < timerAllLessThan) {
            // Flashing lights when the time percentage is less than the threshold
            float flashIntensity = Mathf.PingPong(Time.time * Parameter1, Parameter2); // Change the flashing speed by adjusting the second parameter
            emissionColor = Color.Lerp(Color.red, Color.white, flashIntensity);
        } else {
            if (timePercentage > 0.5f) {
                // From blue to yellow (first half of the time)
                float blueToYellow = (timePercentage - 0.5f) * 2;
                emissionColor = Color.Lerp(Color.magenta, Color.blue, blueToYellow);
            } else {
                // From yellow to red (second half of the time)
                float yellowToRed = timePercentage * 2;
                emissionColor = Color.Lerp(Color.red, Color.magenta, yellowToRed);
            }
        }

        // Apply the color to the material's emission
        material.SetColor("_EmissiveColor", emissionColor * material.GetFloat("_EmissiveIntensity"));
    }

    private IEnumerator SmoothlyUpdateRotationSpeeds() {
        while (IstimeCounting) {
            UpdateRotationSpeed();
            yield return new WaitForSeconds(0.1f); // Adjust the interval for slower updates
        }
    }

    }
}