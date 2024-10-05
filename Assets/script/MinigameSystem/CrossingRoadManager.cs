using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadManager : MonoBehaviour
{
    [Header("SetPlayerProperty")]
    public List<Transform> PlayerSpawnerposition;
    public List<GameObject> PlayerList;
    public bool IsplayerinList,IscrossingRoadStarted,IsPlayerReady;
    public GameObject Player1, Player2;

    [Header("SetPlayerProperty")]
    public List<GameObject> CarsObjList;
    public List<Transform> CarSpawnpoint,CarDestinationPoint;
    public float SpawnDuration;
    

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerReady = false;
        IscrossingRoadStarted = false;
        IsplayerinList = false;
    }

    // Update is called once per frame
    void Update()
    {
       if( IsplayerinList == false)
        {
            FindPlayer();
            Player1 = PlayerList[0];
            Player2 = PlayerList[1];
        }
        else
        {
            InnitializingCrossing();
        }
    }
    public void FindPlayer()
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
    public void InnitializingCrossing()
    {
        TranformPlayertoSetedPlace();
    }
    public void TranformPlayertoSetedPlace()
    {
        if (!IsPlayerReady)
        {
            Player1.transform.position = PlayerSpawnerposition[0].position;
            Player2.transform.position = PlayerSpawnerposition[1].position;
            IsPlayerReady = true;
        }
        else
        {
            InnitializingCarSpawningSystem();
        }
       
    }
    public void InnitializingCarSpawningSystem()
    {

    }
}
