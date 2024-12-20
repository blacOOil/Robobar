using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Timemanager;

public class PlayerScoreSystem : MonoBehaviour
{
    public Gamestate gamestate;
    public TMP_Text PlayerScore;
    public string PlayerTag,PlayerScoreText;

    public int PersonalScore;
    public float Time;
    public TimerCode timerCode;

    public CrossingRoadPlayer crossing;
    // Start is called before the first frame update
    void Start()
    {

        PersonalScore = 0;
         gamestate = GameObject.Find("LevelManager").GetComponent<Gamestate>();
        crossing = gameObject.GetComponent<CrossingRoadPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTag = gameObject.tag;
       
        UpdateScoreOnState();

    }
    public void UpdateScoreOnState()
    {
        switch (gamestate.gamestate_Number)
        {
            case 1:
                CleaningSessionScoring();
                GettimerCode();
                break;
            case 2:
                MiniSessionScoring();
                GettimerCode();
                break;
            case 3:
                gameObject.GetComponent<BotController>().enabled = true;
                crossing.enabled = false;
                GettimerCode();
                break;
            case 4:
                MainSessionScoring();
                GettimerCode();
                break;
            default:
                // Optional: Handle an invalid state if needed
                break;
        }
    }
    public void CleaningSessionScoring()
    {
        
    }
    public void MiniSessionScoring()
    {
        crossing.enabled = true;
    }
    public void MainSessionScoring()
    {
        
    }
    public void ScoreAdd()
    {
        PersonalScore += 1;
    }
    public void GettimerCode()
    {
      
        timerCode = GameObject.Find("LevelManager").GetComponent<TimerCode>();
        if(timerCode.remainingTime <= 0)
        {
            UpdateplayerTag();
        }
    }
    public void UpdateplayerTag()
    {
        if (PlayerTag == "Player1")
        {
            GameObject PlayerScorePlace = GameObject.Find("P1_Score");

            if (PlayerScorePlace != null)
            {
                PlayerScoreText = PersonalScore.ToString();
                PlayerScorePlace.GetComponent<TMP_Text>().text = PlayerScoreText;
            }
            else
            {
            }
        }
        if (PlayerTag == "Player2")
        {
            GameObject PlayerScorePlace = GameObject.Find("P2_Score");

            if (PlayerScorePlace != null)
            {
                PlayerScoreText = PersonalScore.ToString();
                PlayerScorePlace.GetComponent<TMP_Text>().text = PlayerScoreText;
            }
            else
            {
            }
        }
        if (PlayerTag == "Player3")
        {
            GameObject PlayerScorePlace = GameObject.Find("P3_Score");

            if (PlayerScorePlace != null)
            {
                PlayerScoreText = PersonalScore.ToString();
                PlayerScorePlace.GetComponent<TMP_Text>().text = PlayerScoreText;
            }
            else
            {

            }
        }
    }
    
}
