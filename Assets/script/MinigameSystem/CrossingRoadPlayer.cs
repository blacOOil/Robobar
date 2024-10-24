using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadPlayer : MonoBehaviour
{

    public Transform Spawnpoint, TempSpawner, Hand;
    public bool Ispawnset,Isholded;
    private float SpawnerCheckerRadius = 1f;
    private int Playernum = 0;
    public LayerMask SpawnedLayer,Boxlayer;
    public GameObject Objholding;
    public BoxSingle boxSingle;

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
    public bool IspBoxCloseChecked()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpawnerCheckerRadius, SpawnedLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Collector1"))
            {
               
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Ispawnset = false;
        Isholded = false;
        if (gameObject.tag == "Player2")
        {
            Playernum = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IspawnChecked())
        {
            Spawnpoint = TempSpawner;
            Ispawnset = true;
        }
        if (Ispawnset == true)
        {

        }
        if(Isholded && PlayerInput(Playernum))
        {
            if (IspBoxCloseChecked())
            {
                boxSingle = Objholding.GetComponent<BoxSingle>();
                boxSingle.ReturntoSpawnPoint();
                IncreaseeScore();
            }
            ReleaseDrink();
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (Isholded)
            {
                ReleaseDrink();
            }
            gameObject.transform.position = Spawnpoint.transform.position;
          
        }
        if (other.CompareTag("Box1"))
        {
            Isholded = true;
            TransformObjToHand(other.gameObject);
        }
    }
    private void TransformObjToHand(GameObject Obj)
    {
        // Set the drink's position and parent to the hand
        Obj.transform.SetParent(Hand);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        Objholding = Obj;
    }
    public void ReleaseDrink()
    {
        Isholded = false;
        Objholding.GetComponent<Rigidbody>().isKinematic = false;
        Objholding.transform.SetParent(null);
        Objholding.GetComponent<Collider>().isTrigger = true;

    }
    public bool PlayerInput(int playernum)
    {
        if (playernum == 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                return true;
            }
        }
        if (playernum == 1)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                return true;
            }
        }
        return false;
    }
    public void IncreaseeScore()
    {

    }
}
