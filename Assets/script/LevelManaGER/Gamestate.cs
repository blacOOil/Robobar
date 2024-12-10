using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;
using Unity.VisualScripting;

public class Gamestate : MonoBehaviour
{
    public List<GameObject> PlayerList;
    public List<Transform> PlayerServiceSpawnList;
    public bool IsplayerinList,IsplayerPositionseted,IstimerSeted,IsCleaningStateCounted,IsminigameStateCouted,IsResumeSessionCounted;
    public int gamestate_Number , numbertorandom;
    public GameObject Player1, Player2,Player3;
    public TimerCode timerCode;
    public float remainingTime = 10f;
    public AudioCode audioCode;

    // Start is called before the first frame update
    void Start()
    {
        gamestate_Number = 0;
        IsplayerinList = false;
        IsplayerPositionseted = true;
        IstimerSeted = false;
    }

    // Update is called once per frame
    void Update() {
        HandleGameLoop();

        if (timerCode.remainingTime <= 0 && gamestate_Number != 0) {
            if (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.I)) || (Input.GetKeyDown(KeyCode.Keypad9)) || Input.GetButton("Player1Action")) {
                please_next();
              HandleGameState();
                timerCode.resettimer();
            }
        }
    }

    private void HandleGameLoop() {
        if(gamestate_Number >= 5)
        {
            gamestate_Number = 1;
        }
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
        IstimerSeted = false;

        if (audioCode != null) {
            audioCode.PlayAudioForState(gamestate_Number); // Delegate management to AudioCode
        }
    }

    // Placeholder for cleaning session logic
    public void StartCleaningSession() {
        Player1.GetComponent<ServiceSystem>().enabled = false;
        Player2.GetComponent<ServiceSystem>().enabled = false;
        Player3.GetComponent<ServiceSystem>().enabled = false;
        Debug.Log("Cleaning session started");
        Retiming();
    }
    
    // Placeholder for minigame session logic
    public void StartminigameSession() {
        IsplayerPositionseted = false;
        Player1.GetComponent<ServiceSystem>().enabled = false;
        Player2.GetComponent<ServiceSystem>().enabled = false;
        Player3.GetComponent<ServiceSystem>().enabled = false;
        Debug.Log("Minigame session started");
        Retiming();
    }

    // Placeholder for upgrading session logic
    public void UpgradingSession() {
        if(IsplayerPositionseted == false) {
            SetPlayerPosition();
        } else {
        
        }

        Debug.Log("Upgrading session started");
        // Implement upgrading session logic here
    }

    // Placeholder for resume session logic
    public void ResumeSession() {
        Player1.GetComponent<ServiceSystem>().enabled = true;
        Player2.GetComponent<ServiceSystem>().enabled = true;
        Player3.GetComponent<ServiceSystem>().enabled = true;
        Retiming();

        Debug.Log("Resume session started");
        // Implement resume session logic here
    }

    public void AddPlayerToPlayerList() {
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
        GameObject[] Player3= GameObject.FindGameObjectsWithTag("Player3");
        foreach (GameObject player in Player3)
        {
        PlayerList.Add(player);
        }
        IsplayerinList = true;
    }

    public void SetPlayerPosition() {
        Player1.transform.position = PlayerServiceSpawnList[0].position;
        Player2.transform.position = PlayerServiceSpawnList[1].position;
        Player3.transform.position = PlayerServiceSpawnList[2].position;
        IsplayerPositionseted = true;
    }

    public void Retiming() {
        if(IstimerSeted == false)
        {
            timerCode.remainingTime = timerCode.startedRemainingtime;
            timerCode.IstimeCounting = true;
            IstimerSeted = true;
        } else {

        }
    }

    public void InditialePlaying() {
        if (IsplayerinList == false) {
            AddPlayerToPlayerList();
            Player1 = PlayerList[0];
            Player2 = PlayerList[1];
            Player3 = PlayerList[2];
        }
        gamestate_Number = 4;
    }

    public void StartTimeCouting() {
        if (!timerCode.IstimeCounting) // Start countdown only if not already counting
        {
            timerCode.remainingTime = remainingTime;
            timerCode.IstimeCounting = true;
            StartCoroutine(CountdownToNextState());
        }
    }

    // Coroutine to handle countdown and transition
    private IEnumerator CountdownToNextState() {
        while (timerCode.remainingTime > 0)
        {
            yield return null; // Wait for the next frame
        }

        // Ensure time has stopped counting before transitioning
        if (timerCode.remainingTime <= 0)
        {
            timerCode.IstimeCounting = false; // Stop the timer
        }
    }
} 

