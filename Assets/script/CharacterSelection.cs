using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;
    public bool isCharacterSelected,IsReadyToPlay,IsplayerSpawned;
    public GameObject CharSelectedZone,MainGameUi,SelectingUi,CharacterSelector;
    public List<GameObject> CharacterList;
    public Transform PlayerSpawnPoint;

    public List<Transform> CharacterTranformList;
    public int selectorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
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
