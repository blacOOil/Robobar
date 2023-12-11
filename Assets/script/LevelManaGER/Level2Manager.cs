using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2Manager : MonoBehaviour
{
    public GameObject Player, EndScreen, MissionFail;
    [SerializeField] TextMeshProUGUI RemainingOBJ, Timetext, TimeScore, Score;
    [SerializeField] public float remaingTime;
    private float StartingTime, TimeTaken;
    public float Dropped;
    public float TargetPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartingTime = remaingTime;
        Time.timeScale = 1f;

        EndScreen.SetActive(false);
        MissionFail.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        RemainingOBJ.text = string.Format("Deliver {0} out of 10", Dropped);
        remaingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remaingTime / 60);
        int second = Mathf.FloorToInt(remaingTime % 60);
        Timetext.text = string.Format("{0:00}:{1:00}", minutes, second);
        if (remaingTime == 0)
        {
            Timetext.text = "00:00";
            Fail();
        }
        else if (Dropped >= TargetPoint)
        {
            EndLevel();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetTag"))
        {
            Dropped++;
            RemainingOBJ.text = Dropped.ToString();
        }
    }
    void EndLevel()
    {
        ScoreCalculatiom();
        EndScreen.SetActive(true);
    }
    void Fail()
    {


        MissionFail.SetActive(true);

    }
    public void ScoreCalculatiom()
    {
        float a = 60
            , b = 70
            , c = 80
            , d = 90
            ;
        TimeTaken = StartingTime - remaingTime;
        int minutes = Mathf.FloorToInt(TimeTaken / 60);
        int second = Mathf.FloorToInt(TimeTaken % 60);
        TimeScore.text = string.Format("{0:00}:{1:00}", minutes, second);
        if (TimeTaken <= a)
        {
            Score.text = "A";
        }
        else if (TimeTaken <= b)
        {
            Score.text = "B";
        }
        else if (TimeTaken <= c)
        {
            Score.text = "C";
        }
        else if (TimeTaken <= d)
        {
            Score.text = "D";
        }

    }
}

