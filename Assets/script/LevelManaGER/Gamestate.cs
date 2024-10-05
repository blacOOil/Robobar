using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public List<GameObject> PlayerList;
    public bool IsplayerinList;
    public int gamestate_Number = 0, numbertorandom;
    public GameObject Player1, Player2;

    // Start is called before the first frame update
    void Start()
    {
        IsplayerinList = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameState();
    }

    // Handle the game state based on gamestate_Number
    private void HandleGameState()
    {
        switch (gamestate_Number)
        {
            case 1:
                StartCleaningSession();
                break;
            case 2:
                StartminigameSession();
                break;
            case 3:
                UpgradingSession();
                break;
            case 4:
                ResumeSession();
                break;
            default:
                // Optional: Handle an invalid state if needed
                break;
        }
    }

    // Move to the next game state
    public void please_next()
    {
        gamestate_Number++;
    }

    // Placeholder for cleaning session logic
    public void StartCleaningSession()
    {
        if (IsplayerinList == false)
        {
            AddPlayerToPlayerList();
            Player1 = PlayerList[0];
            Player2 = PlayerList[1];

            Player1.GetComponent<ServiceSystem>().enabled = false;
            Player2.GetComponent<ServiceSystem>().enabled = false;
            Debug.Log("Cleaning session started");
            // Implement cleaning session logic here
        }
    }
        // Placeholder for minigame session logic
        public void StartminigameSession()
        {


        Debug.Log("Minigame session started");
        }

        // Placeholder for upgrading session logic
        public void UpgradingSession()
        {
            Debug.Log("Upgrading session started");
            // Implement upgrading session logic here
        }

        // Placeholder for resume session logic
        public void ResumeSession()
        {
            Debug.Log("Resume session started");
            // Implement resume session logic here
        }
        public void AddPlayerToPlayerList()
        {
            GameObject[] Player1 = GameObject.FindGameObjectsWithTag("Player1");
            foreach (GameObject player in Player1)
            {
                PlayerList.Add(player);
            }
            GameObject[] Player2 = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject player in Player2)
            {
                PlayerList.Add(player);
            }
            IsplayerinList = true;
        }
    } 

