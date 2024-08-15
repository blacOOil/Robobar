using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;
using Photon.Pun;

public class SelectionOnline : MonoBehaviourPunCallbacks
{
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
    [Header("OnlineSsion")]
    public Transform PlayerItemPrefabTranform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
