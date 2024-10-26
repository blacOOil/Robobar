using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 namespace Timemanager { 
public class TimerCode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] public float remainingTime,startedRemainingtime;
    [SerializeField] public GameObject GameOverUI;
        public bool IstimeCounting;
        public Material material;

    void Start()
    {
            startedRemainingtime = remainingTime;
            IstimeCounting = true;
            GameOverUI.SetActive(false);
            if (material != null)
            {
                material.EnableKeyword("_EMISSION");
                UpdateEmissionColor();
            }
            // elapsedTime += Time.deltaTime;
            //  TimeText.text = elapsedTime.ToString();

        }

    // Update is called once per frame
    void Update()
    {
        if(IstimeCounting == true)
            {
                CountingDown();
                UpdateEmissionColor();
            }
            else
            {

            }

        if (remainingTime <= 0)
        {
            TimeText.text = string.Format("{0:00}:{1:00}", 0, 0);
                GameOverUI.SetActive(true);
                IstimeCounting = false;
        }
      
    }  
    public void Pause()
        {
            Time.timeScale = 0;

        }
    public void resettimer()
        {
            IstimeCounting = false;
            remainingTime = startedRemainingtime;
            GameOverUI.SetActive(false);
            
        }
    public void CountingDown()
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int second = Mathf.FloorToInt(remainingTime % 60);
            TimeText.text = string.Format("{0:00}:{1:00}", minutes, second);
        }
    private void UpdateEmissionColor()
        {
            // Calculate the percentage of remaining time
            float timePercentage = remainingTime / startedRemainingtime;

            // Determine the color based on the time percentage
            Color emissionColor;

            if (timePercentage > 0.5f)
            {
                // From blue to yellow (first half of the time)
                float blueToYellow = (timePercentage - 0.5f) * 2;
                emissionColor = Color.Lerp(Color.magenta, Color.blue, blueToYellow);
            }
            else
            {
                // From yellow to red (second half of the time)
                float yellowToRed = timePercentage * 2;
                emissionColor = Color.Lerp(Color.red, Color.magenta, yellowToRed);
            }

            // Apply the color to the material's emission
            material.SetColor("_EmissiveColor", emissionColor * material.GetFloat("_EmissiveIntensity"));
        }

    }
}