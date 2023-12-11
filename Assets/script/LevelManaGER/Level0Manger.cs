using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level0Manger : MonoBehaviour
{
    public GameObject Player,EndScreen,MissionFail,Timer;
    [SerializeField] TextMeshProUGUI RemainingOBJ,Timetext,TimeScore,Score;
    [SerializeField] public float remainingTime;
    private float StartingTime, TimeTaken;
    public float Dropped ;
    public float TargetPoint;
    private bool IsFlip = false;
    public BoxCollider Carcollider;
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        StartingTime = remainingTime;
        Time.timeScale = 1f;
        Timer.SetActive(false);
        playerRigidbody = Player.GetComponent<Rigidbody>();
        EndScreen.SetActive(false);
        MissionFail.SetActive(false);
        ResetPlayerConstraints();

    }

    // Update is called once per frame
    void Update()
    {
        //RemainingOBJ.text = string.Format("{0}", Dropped);
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        Timetext.text = string.Format("{0:00}:{1:00}", minutes, second);
        if(remainingTime == 0)
        {
            Timetext.text = "00:00";
            Fail();
        }
        if(IsFlip == true)
        {
            RemainingOBJ.text = "Hold R to Restart";
        }
        else if(Dropped == 0)
        {
            RemainingOBJ.text = "Pick Up a Client";
        }
        else if (Dropped == 1)
        {
            RemainingOBJ.text = "Avoid The Spottlight";
        }
        else if (Dropped == 2)
        {
            RemainingOBJ.text = " Find other Way";
        }
        else if(Dropped == 3)
        {
            RemainingOBJ.text = "Helicopter Run!";
        }

        else if (Dropped >= TargetPoint)
        {
            EndLevel();
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("TargetTag"))
        {
            Timer.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetTag"))
        {
            Dropped++;
            RemainingOBJ.text = Dropped.ToString();
        }
        if (other.CompareTag("InstanceSpotlightTag"))
        {
            Fail();
            RemainingOBJ.text = "You Got Spotted Hold R to Restart";
        }
       
        if (other.CompareTag("Road"))
        {
            IsFlip = true;
        }
        if (other.CompareTag("CutSceneTrigger"))
        {
            Timer.SetActive(true);
        }
    }
    void EndLevel()
    { 
        ScoreCalculatiom();
        EndScreen.SetActive(true);
       
        pause();
    }
    void pause()
    {
        Time.timeScale = 0;
    }
    void Fail()
    {
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        //   WaitForProgress();
        MissionFail.SetActive(true);
        //pause();
    }
    IEnumerator WaitForProgress()
    {
        yield return new WaitForSeconds(10000f);

    }
    void ResetPlayerConstraints()
    {
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }
    public void ScoreCalculatiom()
    {
        float a = 60
            , b = 70
            , c = 80
            , d = 90
            ;
        TimeTaken = StartingTime - remainingTime;
        int minutes = Mathf.FloorToInt(TimeTaken / 60);
        int second = Mathf.FloorToInt(TimeTaken % 60);
        TimeScore.text = string.Format("{0:00}:{1:00}", minutes, second);
        if(TimeTaken >= a )
        {
            Score.text = "A";
        }else if(TimeTaken >= b && TimeTaken >= a)
        {
            Score.text = "B";
        }else if(TimeTaken <= c)
        {
            Score.text = "C";
        }else if(TimeTaken <= d)
        {
            Score.text = "D";
        }

    }
}
