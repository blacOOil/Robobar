using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CrossingRoadManager : MonoBehaviour
{
    [Header("SetPlayerProperty")]
    public List<Transform> PlayerSpawnerposition;
    public List<GameObject> PlayerList;
    public bool IsplayerinList,IscrossingRoadStarted,IsPlayerReady,IsgameAlready;
    public GameObject Player1, Player2,Player3;
    public Gamestate gamestate;

    [Header("SetGameProperty")]
    public List<GameObject> CarsObjList;
    public List<Transform> CarSpawnpoint,CarDestinationPoint;
    public float SpawnDuration = 0.5f;
    public float CarLifetime = 100f;
    public float spawnTimer = 0f,PlayTime = 0f;


    [Header("SetManager")]
    public TimerCode timerCode;
    public float remainingTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameObject.Find("LevelManager").GetComponent<Gamestate>();
        IsPlayerReady = false;
        IscrossingRoadStarted = false;
        IsplayerinList = false;
        IsgameAlready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsplayerinList == false)
        {
            FindPlayer();
            Player1 = PlayerList[0];
            Player2 = PlayerList[1];
            Player3 = PlayerList[2];
            InnitializingCrossing();
        }
        else
        {

        }
        if (IscrossingRoadStarted == true)
        {

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= SpawnDuration) // Check if enough time has passed
            {
                SpawnCar(); // Spawn the car
                spawnTimer = 0f; // Reset the timer
            }
        }
        if (timerCode.remainingTime <= 0 )
        {
            Debug.Log("gameover");
            GameOver();
            IsgameAlready = true;
        }
       
       
       
    }
    
    public void GameOver()
    {
        PlayTime++;
        IscrossingRoadStarted = false;
        IsPlayerReady = false;

    }
    public void RestartGame()
    {
        Debug.Log("RE");
        
      //  IsPlayerReady = false;
        InnitializingCrossing();


        // Reset the IsgameAlready flag so the game can run again
        IsgameAlready = false;
        

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
        GameObject[] Player3 = GameObject.FindGameObjectsWithTag("Player3");
        foreach (GameObject player in Player3)
        {
            PlayerList.Add(player);
        }

        IsplayerinList = true;
    }
    public void InnitializingCrossing()
    {
        TranformPlayertoSetedPlace();
        IscrossingRoadStarted = true;
      
    }
    public void ResetPlayerPositions()
    {
        Debug.Log("RePosition");
        Player1.transform.position = PlayerSpawnerposition[0].position;
        Player2.transform.position = PlayerSpawnerposition[1].position;
        Player3.transform.position = PlayerSpawnerposition[2].position;
        IsPlayerReady = true; // Mark players as ready
    }
    public void TranformPlayertoSetedPlace()
    {
        if (!IsPlayerReady)
        {
            Player1.transform.position = PlayerSpawnerposition[0].position;
            Player2.transform.position = PlayerSpawnerposition[1].position;
            Player3.transform.position = PlayerSpawnerposition[1].position;
            IsPlayerReady = true;
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
            StartCoroutine(MoveCarToDestination(car, randomSpawnIndex));

            // Destroy the car after some time
            Destroy(car, CarLifetime);
        }
    }

    private IEnumerator MoveCarToDestination(GameObject car,int randomSpawnIndex)
    {
        int randomDestinationIndex = randomSpawnIndex;
        Transform destination = CarDestinationPoint[randomDestinationIndex];

        // Set the speed of the car
        float moveSpeed = 5f; // Adjust speed here
        float rotationSpeed = 1000f; // Adjust rotation speed here

        while (car != null && Vector3.Distance(car.transform.position, destination.position) > 0.1f)
        {
            // Move the car towards the destination
            car.transform.position = Vector3.MoveTowards(car.transform.position, destination.position, moveSpeed * Time.deltaTime);

            // Calculate the rotation step
            Vector3 direction = (destination.position - car.transform.position).normalized; // Get the direction to the destination
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Create a rotation that points in that direction

            // Rotate the car smoothly towards the destination
            car.transform.rotation = Quaternion.RotateTowards(car.transform.rotation, lookRotation, rotationSpeed );

            yield return null; // Wait for the next frame
        }
    }
  
}

