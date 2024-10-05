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
            
            RandomMinigameMethod();
            if(IsminigameStarted = true)
            {
                InnitializingMinigame();
            }
            else
            {

            }
        }
        else
        {
            ClearMinigame();
        }
    }
    public void ClearMinigame()
    {

    }
    public void UpdateGamestate()
    {
        gamestatenumber = gamestate.gamestate_Number;
    }
    public void RandomMinigameMethod()
    {
        RandomMinigameint = Random.Range(0, MinigameList.Count - 1);
        IsminigameStarted = true;
    }
    public void InnitializingMinigame()
    {
        GameObject selectedMinigame = MinigameList[RandomMinigameint];
        selectedMinigame.SetActive(true);
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
