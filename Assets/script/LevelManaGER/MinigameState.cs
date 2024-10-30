using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameState : MonoBehaviour
{
    public List<GameObject> MinigameList;
    public bool IsminigameStarted;
    private int RandomMinigameint, gamestatenumber;
    public Gamestate gamestate;
    // Start is called before the first frame update
    void Start()
    {
        IsminigameStarted = false;
        SetMinigamesInactive();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamestate();
        if(gamestatenumber == 2)
        {
            
            if(!IsminigameStarted)
            {
                RandomMinigameMethod();
                InnitializingMinigame();
                
            }
            else
            {

            }
        }
        else
        {
            ClearMinigame();
            IsminigameStarted = false;
        }
    }
    public void ClearMinigame()
    {
        SetMinigamesInactive();
    }
    public void UpdateGamestate()
    {
        gamestatenumber = gamestate.gamestate_Number;
    }
    public void RandomMinigameMethod()
    {
        RandomMinigameint = Random.Range(0, MinigameList.Count - 1);
      //  IsminigameStarted = false;
    }
    public void InnitializingMinigame()
    {
      
        GameObject selectedMinigame = MinigameList[RandomMinigameint];
        selectedMinigame.SetActive(true);
        if (RandomMinigameint == 0)
        {
           
            CrossingRoadManager crossingRoadManager = MinigameList[RandomMinigameint].GetComponent<CrossingRoadManager>();
            if (crossingRoadManager.PlayTime>=1 && !IsminigameStarted  )
            {
                crossingRoadManager.ResetPlayerPositions();
                crossingRoadManager.IsPlayerReady = false;
                crossingRoadManager.RestartGame();
             
            }

        }

        IsminigameStarted = true;


    }
    public void SetMinigamesInactive()
    {
        foreach (GameObject minigame in MinigameList)
        {
            if (minigame != null)
            {
                minigame.SetActive(false);  // Set each minigame GameObject to inactive
            }
        }
    }
}
