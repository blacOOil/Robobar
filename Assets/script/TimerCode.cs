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

    void Start()
    {
            startedRemainingtime = remainingTime;
            IstimeCounting = true;
            GameOverUI.SetActive(false);
       // elapsedTime += Time.deltaTime;
      //  TimeText.text = elapsedTime.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if(IstimeCounting == true)
            {
                CountingDown();
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
}
}