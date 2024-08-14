using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    [Header("Local-Coop Logic")]
    public GameObject Local_CoopSelecterPrefab, PlayerTwoSelecting = null;
    public bool IslocalCoopGame, Isplayer2Selected, Isselector2Spawned, IsPlayer2Spawned, Is2pawned = false;
    public Transform Player2SpawnerTranform;
    public int Player2SelectedNumber = 0;
    [Header("Gameplay")]
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;
    [Header("SpawningLogic")]
    public bool isCharacterSelected, IsReadyToPlay, IsplayerSpawned;
    public GameObject CharSelectedZone, MainGameUi, SelectingUi;
    public List<GameObject> CharacterList;
    public Transform PlayerSpawnPoint, HidePlace;
    [Header("UiSesssion")]
    public List<GameObject> MultiplayerUIList;
    public int numberofPlayer;
    public bool IsGamealreadyPlay, IsPlayer2active, IsPlayer3active, IsPlayer4active;
    [Header("Character List")]
    public List<Transform> CharacterTranformList;
    public int selectorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        IsGamealreadyPlay = false;
        numberofPlayer = 1;

        IsPlayer2active = true;


        Isselector2Spawned = false;
        Isplayer2Selected = false;

        IsPlayer2Spawned = false;
        IsplayerSpawned = false;

        isCharacterSelected = false;
        timerCode.enabled = false;
        monneyLevelCode.enabled = false;
        MainGameUi.SetActive(false);
        IsReadyToPlay = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCharacterSelected)
        {
            CharacterSelecting(0);
        }
        if (!Isplayer2Selected)
        {
            CharacterSelecting(1);
        }
    }
    public void SelectorMoveLeft(int playernum)
    {
        if (playernum == 0)
        {
            if (selectorNumber > 0)
            {
                selectorNumber--;
                CharacterSelecting(playernum);
            }
        }
        if (playernum == 1)
        {
            if (Player2SelectedNumber > 0)
            {
                Destroy(PlayerTwoSelecting);
                Is2pawned = false;
                Player2SelectedNumber--;
                CharacterSelecting(playernum);
            }
        }

    }
    public void SelectorMoveRight(int playernum)
    {
        if (playernum == 0)
        {
            if (selectorNumber < CharacterList.Count - 1)
            {
                selectorNumber++;
                CharacterSelecting(playernum);
            }
        }
        if (playernum == 1)
        {

            if (Player2SelectedNumber < CharacterList.Count - 1)
            {
                Destroy(PlayerTwoSelecting);
                Is2pawned = false;
                Player2SelectedNumber++;
                CharacterSelecting(playernum);
            }
        }
    }
    public void SpawnPlayer()
    {
        if (!IsplayerSpawned)
        {
            CharacterList[selectorNumber].transform.position = PlayerSpawnPoint.position;
            IsplayerSpawned = true;
        }

        foreach (GameObject character in CharacterList)
        {
            BotController botController = character.GetComponent<BotController>();
            botController.enabled = true;
            ServiceSystem service = character.GetComponent<ServiceSystem>();
            service.enabled = true;

        }

    }

    public void StartGameNow()
    {
        timerCode.enabled = true;
        monneyLevelCode.enabled = true;
        MainGameUi.SetActive(true);
        CharSelectedZone.SetActive(false);
        SpawnPlayer();
        IsGamealreadyPlay = true;
    }
    public void ReadytoPlay()
    {
        isCharacterSelected = true;
        IsReadyToPlay = true;
        StartGameNow();
    }
    public void Spawnplayer2()
    {
        CharacterList[Player2SelectedNumber].transform.position = Player2SpawnerTranform.position;
        BotController botController = CharacterList[Player2SelectedNumber].GetComponent<BotController>();
        botController.inputNameHorizontal = "Horizontal2";
        botController.inputNameVertical = "Vertical2";
        IsPlayer2Spawned = true;
    }
    public void CharacterSelecting(int PlayerNum)
    {
        if (PlayerNum == 0)
        {
            Character1Selecting();
        }
        if (PlayerNum == 1)
        {
            Character2Selecting(PlayerNum);
        }


    }
    public void Character1Selecting()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == selectorNumber)
            {
                // Set the selected character's position
                CharacterList[i].transform.position = CharacterTranformList[0].transform.position;
                CharacterList[i].transform.rotation = CharacterTranformList[0].transform.rotation;

            }
            else
            {
                // Deactivate the other characters
                CharacterList[i].transform.position = HidePlace.position;
            }

        }
    }
    public void Character2Selecting(int playernum)
    {
        ;
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == Player2SelectedNumber)
            {
                if (!Is2pawned)
                {
                    PlayerTwoSelecting = Instantiate(CharacterList[i], CharacterTranformList[playernum].transform.position, CharacterTranformList[playernum].transform.rotation);
                    Is2pawned = true;

                }

            }


        }
    }
}
