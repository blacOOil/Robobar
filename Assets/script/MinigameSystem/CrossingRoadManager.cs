using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CrossingRoadManager : MonoBehaviour
{
    [Header("SetPlayerProperty")]
    public List<Transform> PlayerSpawnerposition;
    public List<GameObject> PlayerList;
    public bool IsplayerinList,IscrossingRoadStarted,IsPlayerReady;
    public GameObject Player1, Player2;

    [Header("SetGameProperty")]
    public List<GameObject> CarsObjList;
    public List<Transform> CarSpawnpoint,CarDestinationPoint;
    public float SpawnDuration = 3f;
    public float CarLifetime = 100f;

    [Header("SetManager")]
    public TimerCode timerCode;
    public float remainingTime = 10f;


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
            InnitializingCrossing();
        }
        else
        {
           
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
        IscrossingRoadStarted = true;
        StartTimeCouting();
    }
    public void TranformPlayertoSetedPlace()
    {
        if (!IsPlayerReady)
        {
            Player1.transform.position = PlayerSpawnerposition[0].position;
            Player2.transform.position = PlayerSpawnerposition[1].position;
            IsPlayerReady = true;
        }    
    }
    public IEnumerator CarSpawningSystem()
    {
        while (true)
        {
            if (IscrossingRoadStarted) // Only spawn cars when the crossing has started
            {
                SpawnCar();
            }
            yield return new WaitForSeconds(SpawnDuration);
        }
    }

    public void SpawnCar()
    {
        if (CarsObjList.Count > 0 && CarSpawnpoint.Count > 0 && CarDestinationPoint.Count > 0)
        {
            // Select a random car and spawn point
            int randomCarIndex = Random.Range(0, CarsObjList.Count);
            int randomSpawnIndex = Random.Range(0, CarSpawnpoint.Count);

            // Instantiate the car at the selected spawn point
            GameObject car = Instantiate(CarsObjList[randomCarIndex], CarSpawnpoint[randomSpawnIndex].position, Quaternion.identity);

            // Move the car to the destination
            StartCoroutine(MoveCarToDestination(car));

            // Destroy the car after some time
            Destroy(car, CarLifetime);
        }
    }

    private IEnumerator MoveCarToDestination(GameObject car)
    {
        int randomDestinationIndex = Random.Range(0, CarDestinationPoint.Count);
        Transform destination = CarDestinationPoint[randomDestinationIndex];

        while (car != null && Vector3.Distance(car.transform.position, destination.position) > 0.1f)
        {
            car.transform.position = Vector3.MoveTowards(car.transform.position, destination.position, Time.deltaTime * 5f);  // Adjust speed here
            yield return null;
        }
    }
    public void StartTimeCouting()
    {
        if (timerCode.IstimeCounting != true)
        {
            timerCode.remainingTime = remainingTime;
            timerCode.IstimeCounting = true;
        }
        else
        {

        }

    }
}

