using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadPlayer : MonoBehaviour
{

    public Transform Spawnpoint, TempSpawner, Hand;
    public bool Ispawnset,Isholded;
    private float SpawnerCheckerRadius = 1f;
    public LayerMask SpawnedLayer;
    public GameObject Objholding;

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
    // Start is called before the first frame update
    void Start()
    {
        Ispawnset = false;
        Isholded = false;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            gameObject.transform.position = Spawnpoint.transform.position;
            if (Isholded)
            {
                ReleaseDrink();
            }

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
        Objholding.GetComponent<Collider>().isTrigger = false;

    }
}
