using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    [Header("Local-Coop Logic")]
    public GameObject Local_CoopSelecterPrefab, PlayerTwoSelecting = null,PlayerThreeSelecting = null;
    public bool IsntlocalCoopGame, Isplayer2Selected, Isselector2Spawned, IsPlayer2Spawned, Is2pawned = false;
    public bool Isplayer3Selected, Isselector3Spawned, IsPlayer3Spawned, Is3pawned = false;
    public Transform Player2SpawnerTranform,Player3SpawnerTranform;
    public int Player2SelectedNumber = 0,Player3SelectedNumber = 0;

    [Header("Gameplay")]
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;

    [Header("Spawning Logic")]
    public bool isCharacterSelected, IsReadyToPlay, IsplayerSpawned,IsP1ready;
    public GameObject CharSelectedZone, MainGameUi, SelectingUi;
    public List<GameObject> CharacterList;
    public Transform PlayerSpawnPoint, HidePlace;
    public ReadyCoding readyCoding;

    [Header("Ui Session")]
    public List<GameObject> MultiplayerUIList;
    public int numberofPlayer;
    public bool IsGamealreadyPlay, IsPlayer2active, IsPlayer3active, IsPlayer4active;

    [Header("Character List")]
    public List<Transform> CharacterTranformList;
    public int selectorNumber = 0;

    [Header("button")]
    // public List<GameObject> readybutton;
    public Transform UiTranformHide;
    public bool IsAllready;

    public float dPadHorizontal;
    private float inputCooldown = 0.2f; // Cooldown time in seconds
    private float lastInputTime = 0f;   // Tracks the time of the last D-pad input
    // Start is called before the first frame update
    void Start()
    {
        InitializeSettings();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCharacterSelection();
        if(IsP1ready == false)
        {
            dPadHorizontal = Input.GetAxis("DPadHorizonal1");
            if (Time.time - lastInputTime > inputCooldown)
            {
                if (dPadHorizontal == -1 && selectorNumber > 0)
                {
                    selectorNumber--;
                    CharacterSelecting(0);
                    lastInputTime = Time.time;
                }
                if (dPadHorizontal == 1 && (selectorNumber < CharacterList.Count - 1))
                {
                    selectorNumber++;
                    CharacterSelecting(0);
                    lastInputTime = Time.time;
                }
            }
            if (IsAllready)
            {
                if (Input.GetButtonDown("Player1Action"))
                {
                    // readyCoding.ReadyButt(0);
                    // readyCoding.HidSelector(0);
                    HandleBootlePress(0);
                    IsP1ready = true;

                }
            }
        }
        else
        {

        }
       
    }

    // Initialize all game settings
    private void InitializeSettings()
    {
        IsGamealreadyPlay = false;
        numberofPlayer = 1;

        IsP1ready = false;

        IsPlayer2active = true;
        Isselector2Spawned = false;
        Isplayer2Selected = false;
        IsPlayer2Spawned = false;

        IsplayerSpawned = false;
        isCharacterSelected = false;

        IsPlayer3active = true;
        Isselector3Spawned = false;
        Isplayer3Selected = false;
        IsPlayer3Spawned = false;

        timerCode.enabled = false;
        monneyLevelCode.enabled = false;
        MainGameUi.SetActive(false);
        IsReadyToPlay = false;
       
    }

    // Handles character selection based on game type
    private void HandleCharacterSelection()
    {
        if (!isCharacterSelected)
        {
            CharacterSelecting(0);
        }

        if (IsntlocalCoopGame == false)
        {
            if (!Isplayer2Selected)
            {
                CharacterSelecting(1);
            }
            if (!Isplayer3Selected)
            {
                CharacterSelecting(2);
            }
        }
        if ((selectorNumber == Player2SelectedNumber)  || (Player2SelectedNumber == Player3SelectedNumber) || (selectorNumber == Player3SelectedNumber))
        {
          
            HandleSelectedSameCharacter();
        }
        else
        {
            HandleSelecteddifCharacter();
        }
    }

    // Move selector left for a given player number
    public void SelectorMoveLeft(int playernum)
    {
        if (playernum == 0 && selectorNumber > 0)
        {
            selectorNumber--;
            CharacterSelecting(playernum);
        }

        if (playernum == 1 && Player2SelectedNumber > 0)
        {
            Destroy(PlayerTwoSelecting);
            Is2pawned = false;
            Player2SelectedNumber--;
            CharacterSelecting(playernum);
        }
        if (playernum == 2 && Player3SelectedNumber > 0)
        {
            Destroy(PlayerThreeSelecting);
            Is3pawned = false;
            Player3SelectedNumber--;
            CharacterSelecting(playernum);
        }
    }

    // Move selector right for a given player number
    public void SelectorMoveRight(int playernum)
    {
        if (playernum == 0 && selectorNumber < CharacterList.Count - 1)
        {
            selectorNumber++;
            CharacterSelecting(playernum);
        }

        if (playernum == 1 && Player2SelectedNumber < CharacterList.Count - 1)
        {
            Destroy(PlayerTwoSelecting);
            Is2pawned = false;
            Player2SelectedNumber++;
            CharacterSelecting(playernum);
        }
        if (playernum == 2 && Player3SelectedNumber < CharacterList.Count - 1)
        {
            Destroy(PlayerThreeSelecting);
            Is3pawned = false;
            Player3SelectedNumber++;
            CharacterSelecting(playernum);
        }
    }

    // Spawn player 1
    public void SpawnPlayer()
    {
        if (!IsplayerSpawned)
        {
            GameObject player = Instantiate(CharacterList[selectorNumber], PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
            
            player.tag = "Player1";

            BotController botController = player.GetComponent<BotController>();
            botController.enabled = true;
            ServiceSystem service = player.GetComponent<ServiceSystem>();
            service.enabled = true;
            IsplayerSpawned = true;
        }
        

         EnableBotControllers();
    }

    // Enable bot controllers for all characters
    private void EnableBotControllers()
    {
        foreach (GameObject character in CharacterList)
        {
            BotController botController = character.GetComponent<BotController>();
            botController.enabled = true;
            ServiceSystem service = character.GetComponent<ServiceSystem>();
            service.enabled = true;
        }
    }

    // Start the game after character selection
    public void StartGameNow()
    {
        timerCode.enabled = true;
        monneyLevelCode.enabled = true;
        MainGameUi.SetActive(true);
        CharSelectedZone.SetActive(false);
        SpawnPlayer();
        IsGamealreadyPlay = true;

        if (IsPlayer2active)
        {
            Spawnplayer2(2);
        }
        if (IsPlayer3active)
        {
            Spawnplayer2(3);
        }
    }

    // Ready the game for play
    public void ReadytoPlay()
    {
        isCharacterSelected = true;
        IsReadyToPlay = true;
        StartGameNow();
        ClearUnuse();
    }
    public void ClearUnuse()
    {
        GameObject[] clearPlayer = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject bot in clearPlayer)
        {
            bot.SetActive(false);
        }
    }

    // Spawn player 2
    public void Spawnplayer2(int PlayerNum)
    {
        if(PlayerNum == 2)
        {
            GameObject player2 = Instantiate(CharacterList[Player2SelectedNumber], Player2SpawnerTranform.position, Player2SpawnerTranform.rotation);
          
            BotController botController = player2.GetComponent<BotController>();
            botController.enabled = true;
            botController.inputNameHorizontal = "Horizontal2";
            botController.inputNameVertical = "Vertical2";
            player2.tag = "Player2";
            ServiceSystem service = player2.GetComponent<ServiceSystem>();
            service.enabled = true;

            IsPlayer2Spawned = true;
        }
        if(PlayerNum == 3)
        {
            GameObject player3 = Instantiate(CharacterList[Player3SelectedNumber], Player3SpawnerTranform.position, Player3SpawnerTranform.rotation);
           
            BotController botController = player3.GetComponent<BotController>();
            botController.enabled = true;
            botController.inputNameHorizontal = "Horizontal3";
            botController.inputNameVertical = "Vertical3";
            player3.tag = "Player3";
            ServiceSystem service = player3.GetComponent<ServiceSystem>();
            service.enabled = true;
            IsPlayer3Spawned = true;
        }
        
    }

    // Character selection for either player 1 or player 2
    public void CharacterSelecting(int PlayerNum)
    {
        if (readyCoding.playerReadyStatus[PlayerNum])
        {
            Debug.Log($"Player {PlayerNum} is ready and cannot change character.");
            return; // Skip character selection if the player is ready
        }
        if (PlayerNum == 0)
        {
            Character1Selecting();
        }
        else if (PlayerNum == 1)
        {
            Character2Selecting();
        }
        else if (PlayerNum == 2)
        {
            Character3Selecting();
        }

        // Inform ReadyCoding of the selected character
        readyCoding.CharacterSelected(PlayerNum, GetSelectedCharacterID(PlayerNum));
    }

    private int GetSelectedCharacterID(int PlayerNum)
    {
        // Return the ID of the currently selected character for the given player
        // Modify this based on your implementation for identifying character IDs
        if (PlayerNum == 0) return selectorNumber;
        if (PlayerNum == 1) return Player2SelectedNumber;
        if (PlayerNum == 2) return Player3SelectedNumber;
        return -1;
    }

    // Handle player 1 character selection
    public void Character1Selecting()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == selectorNumber)
            {
                CharacterList[i].transform.position = CharacterTranformList[0].transform.position;
                CharacterList[i].transform.rotation = CharacterTranformList[0].transform.rotation;
            }
            else
            {
                CharacterList[i].transform.position = HidePlace.position;
            }
        }
    }

    // Handle player 2 character selection
    public void Character2Selecting()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == Player2SelectedNumber && !Is2pawned)
            {
                PlayerTwoSelecting = Instantiate(CharacterList[i], CharacterTranformList[1].transform.position, CharacterTranformList[1].transform.rotation);
                PlayerTwoSelecting.transform.position = CharacterTranformList[1].transform.position;
                PlayerTwoSelecting.transform.rotation = CharacterTranformList[1].transform.rotation;
                Is2pawned = true;
            }
            else if (i == Player2SelectedNumber && Is2pawned)
            {
                PlayerTwoSelecting.transform.position = CharacterTranformList[1].transform.position;
                PlayerTwoSelecting.transform.rotation = CharacterTranformList[1].transform.rotation;
            }
        }
    
    }
    public void Character3Selecting()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == Player3SelectedNumber && !Is3pawned)
            {
                PlayerThreeSelecting = Instantiate(CharacterList[i], CharacterTranformList[2].transform.position, CharacterTranformList[2].transform.rotation);
                PlayerThreeSelecting.transform.position = CharacterTranformList[2].transform.position;
                PlayerThreeSelecting.transform.rotation = CharacterTranformList[2].transform.rotation;
                Is3pawned = true;
            }
            else if (i == Player3SelectedNumber && Is3pawned)
            {
                PlayerThreeSelecting.transform.position = CharacterTranformList[2].transform.position;
                PlayerThreeSelecting.transform.rotation = CharacterTranformList[2].transform.rotation;
            }
        }
    }
    public void HandleSelectedSameCharacter()
    {
        IsAllready = false;
            readyCoding.readyButton[0].SetActive(false);
            readyCoding.readyButton[1].SetActive(false);
            readyCoding.readyButton[2].SetActive(false);


    }
    public void HandleSelecteddifCharacter()
    {
         IsAllready = true;
            readyCoding.readyButton[0].SetActive(true);
            readyCoding.readyButton[1].SetActive(true);
            readyCoding.readyButton[2].SetActive(true);
    }
    public void HandleBootlePress(int Num)
    {
        readyCoding.readyButton[Num].transform.position = UiTranformHide.position;
       
    }
}
