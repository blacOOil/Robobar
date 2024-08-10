using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    [Header("Local-Coop Logic")]
    public GameObject Local_CoopSelecterPrefab, Local_CoopSelecter;
    public bool IslocalCoopGame, Isplayer2Selected, Isselector2Spawned, IsPlayer2Spawned;
    public Transform Player2SpawnerTranform;
    public int Player2SelectedNumber = 0;
    [Header("Gameplay")]
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;
    [Header("SpawningLogic")]
    public bool isCharacterSelected, IsReadyToPlay, IsplayerSpawned, IsCharExamSpawn = false;
    public GameObject CharSelectedZone, MainGameUi, SelectingUi;
    public List<GameObject> CharacterList;
    public Transform PlayerSpawnPoint;
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

        IsPlayer2active = false;
        IsPlayer3active = false;
        IsPlayer4active = false;

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
            CharacterSelecting();
        }
        if (IsPlayer2active)
        {
            MultiplayerUIList[1].SetActive(true);

            if (!Isselector2Spawned)
            {

            }
            else
            {

            }
            if (Isplayer2Selected)
            {
                if (!IsPlayer2Spawned)
                {
                    Spawnplayer2();
                }
            }
        }
        else
        {

        }
        MultiplayerUIList[2].SetActive(IsPlayer3active);
        MultiplayerUIList[3].SetActive(IsPlayer4active);
    }
    public void SelectorMoveLeft()
    {
        if (selectorNumber > 0)
        {
            IsCharExamSpawn = false;
            selectorNumber--;
            CharacterSelecting();
        }
    }
    public void SelectorMoveRight()
    {
        if (selectorNumber < CharacterList.Count - 1)
        {
            IsCharExamSpawn = false;
            selectorNumber++;
            CharacterSelecting();
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
        Destroy(Local_CoopSelecter);
        IsPlayer2Spawned = true;
    }
    public void CharacterSelecting()
    {
        for (int i = 0; i < CharacterList.Count; i++)
        {
            if (i == selectorNumber)
            {
                // Set the selected character's position
                CharacterList[i].transform.position = CharacterTranformList[0].transform.position;
                CharacterList[i].transform.rotation = CharacterTranformList[0].transform.rotation;
                // Activate the selected character
                CharacterList[i].SetActive(true);
            }
            else
            {
                // Deactivate the other characters
                CharacterList[i].SetActive(false);
            }

        }

    }
}
