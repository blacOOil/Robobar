using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadPlayer : MonoBehaviour
{
    public Transform Spawnpoint, TempSpawner, Hand;
    public bool Ispawnset, IsReached;
    private float SpawnerCheckerRadius = 1f;
    public int Playernum = 0;
    public LayerMask SpawnedLayer, Boxlayer;
    public GameObject Objholding;
    public BoxSingle boxSingle;
    public MonneyLevelCode monneyLevelCode;
    public Gamestate gamestate;

    void Start()
    {
        monneyLevelCode = GameObject.Find("LevelManager").GetComponent<MonneyLevelCode>();
        Ispawnset = false;
        IsReached = false;
        gamestate = GameObject.Find("LevelManager").GetComponent<Gamestate>();
    }

    void Update()
    {
        if(gamestate.gamestate_Number == 2)
        {
            if (gameObject.tag == "Player2")
            {
                Playernum = 1;
            }
            else if (gameObject.tag == "Player3")
            {
                Playernum = 2;
            }
            if (IspawnChecked())
            {
                Spawnpoint = TempSpawner;
                Ispawnset = true;
            }
            if (IsReached == true)
            {
                gameObject.GetComponent<BotController>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BotController>().enabled = true;
            }

        }
        else
        {
            Ispawnset = false;
            IsReached = false;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            transform.position = Spawnpoint.position;
        }

        // Handle player-specific box pickup
        if ((Playernum == 0 && other.CompareTag("Box1")) ||
            (Playernum == 1 && other.CompareTag("Box2")) ||
            (Playernum == 2 && other.CompareTag("Box3")))
        {
            IsReached = true;
        }
    }
   

    public bool IspawnChecked()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpawnerCheckerRadius, SpawnedLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("CrossRoadSpawnPoint"))
            {
                TempSpawner = hitCollider.transform;
                return true;
            }
        }
        return false;
    }

 
    public void IncreaseScore()
    {
        monneyLevelCode.moneyAdd();
    }
}
