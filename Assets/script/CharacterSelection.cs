using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    [Header("Local-Coop Logic")]
    public GameObject Local_CoopSelecterPrefab, Local_CoopSelecter;
    public bool IslocalCoopGame,Isplayer2Selected,Isselector2Spawned,IsPlayer2Spawned;
    public Transform Player2SpawnerTranform;
    public int Player2SelectedNumber = 0;
    [Header("Gameplay")]
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;
    [Header("SpawningLogic")]
    public bool isCharacterSelected,IsReadyToPlay,IsplayerSpawned;
    public GameObject CharSelectedZone,MainGameUi,SelectingUi,CharacterSelector;
    public List<GameObject> CharacterList;
    public Transform PlayerSpawnPoint;

    public List<Transform> CharacterTranformList;
    public int selectorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        Isselector2Spawned = false;
        Isplayer2Selected = false;
        IslocalCoopGame = false;
        IsPlayer2Spawned = false;
        IsplayerSpawned = false;
        
        isCharacterSelected = false;
        timerCode.enabled = false;
        monneyLevelCode.enabled = false;
        MainGameUi.SetActive(false);
        IsReadyToPlay = false;

        foreach(GameObject character in CharacterList)
        {
            CharacterTranformList.Add(character.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharacterSelected)
        {
            timerCode.enabled = true;
            monneyLevelCode.enabled = true;
            MainGameUi.SetActive(true);
            CharSelectedZone.SetActive(false);
            SpawnPlayer();

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D) && (selectorNumber < CharacterTranformList.Count))
            {
                selectorNumber++;
                SelectorMove();
            }
            if (selectorNumber > 0 && (Input.GetKeyDown(KeyCode.A)))
            {
                selectorNumber--;
                SelectorMove();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                IsReadyToPlay = true;
                isCharacterSelected = true;

            }   
        }
        if (IslocalCoopGame)
        {
            if (!Isselector2Spawned)
            {
                Local_CoopSelecter =  Instantiate(Local_CoopSelecterPrefab, CharacterTranformList[0].position, CharacterTranformList[0].rotation);
                Isselector2Spawned = true;
            }
            else
            {
                if (!Isplayer2Selected)
                {
                    if (Input.GetKeyDown(KeyCode.K) && (Player2SelectedNumber < CharacterTranformList.Count - 1))
                    {
                        Player2SelectedNumber++;
                        Local_CoopSelecter.transform.position = CharacterTranformList[Player2SelectedNumber].position;
                    }
                    if (Input.GetKeyDown(KeyCode.H) && (Player2SelectedNumber > 0))
                    {
                        Player2SelectedNumber--;
                        Local_CoopSelecter.transform.position = CharacterTranformList[Player2SelectedNumber].position;
                    }
                    if (Input.GetKeyDown(KeyCode.I) && (Player2SelectedNumber != selectorNumber))
                    {
                        Isplayer2Selected = true;

                    }
                }
            }
            if (Isplayer2Selected)
            {
                if (!IsPlayer2Spawned)
                {
                    CharacterList[Player2SelectedNumber].transform.position = Player2SpawnerTranform.position;
                    Destroy(Local_CoopSelecter);
                    IsPlayer2Spawned = true;
                }
            }
        }

    }
    public void SelectorMove()
    {
        if(selectorNumber >= 0 && (selectorNumber < CharacterTranformList.Count))
        {
            CharacterSelector.transform.position = CharacterTranformList[selectorNumber].position;
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
}
