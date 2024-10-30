using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadPlayer : MonoBehaviour
{
    public Transform Spawnpoint, TempSpawner, Hand;
    public bool Ispawnset, Isholded;
    private float SpawnerCheckerRadius = 1f;
    public int Playernum = 0;
    public LayerMask SpawnedLayer, Boxlayer;
    public GameObject Objholding;
    public BoxSingle boxSingle;
    public MonneyLevelCode monneyLevelCode;

    void Start()
    {
        monneyLevelCode = GameObject.Find("LevelManager").GetComponent<MonneyLevelCode>();
        Ispawnset = false;
        Isholded = false;
    }

    void Update()
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

        if (Isholded && PlayerInput(Playernum))
        {
            if (IspBoxCloseChecked())
            {
                boxSingle = Objholding.GetComponent<BoxSingle>();
                boxSingle.ReturntoSpawnPoint();
                IncreaseScore();
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
            transform.position = Spawnpoint.position;
        }

        // Handle player-specific box pickup
        if ((Playernum == 0 && other.CompareTag("Box1")) ||
            (Playernum == 1 && other.CompareTag("Box2")) ||
            (Playernum == 2 && other.CompareTag("Box3")))
        {
            Isholded = true;
            TransformObjToHand(other.gameObject);
        }
    }

    private void TransformObjToHand(GameObject Obj)
    {
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpawnerCheckerRadius, Boxlayer);
        foreach (var hitCollider in hitColliders)
        {
            if (((Playernum == 0) && hitCollider.CompareTag("Collector1")) || 
                (Playernum == 1) && hitCollider.CompareTag("Collector2") ||
                (Playernum == 2) && hitCollider.CompareTag("Collector3"))
            {
                return true;
            }
        }
        return false;
    }

    public bool PlayerInput(int playernum)
    {
        switch (playernum)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.E);
            case 1:
                return Input.GetKeyDown(KeyCode.I);
            case 2:
                return Input.GetKeyDown(KeyCode.Keypad9);
            default:
                return false;
        }
    }

    public void IncreaseScore()
    {
        monneyLevelCode.moneyAdd();
    }
}
