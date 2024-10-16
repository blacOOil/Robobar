using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingRoadPlayer : MonoBehaviour
{
   
    public Transform Spawnpoint,TempSpawner;
    public bool Ispawnset;
    private float SpawnerCheckerRadius = 1f;
    public LayerMask SpawnedLayer;

    public bool IspawnChecked()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpawnerCheckerRadius, SpawnedLayer);
        foreach(var hitCollider in hitColliders)
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
    }

    // Update is called once per frame
    void Update()
    {
        if(IspawnChecked())
        {
            Spawnpoint = TempSpawner;
            Ispawnset = true;
        }
        if(Ispawnset == true)
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            gameObject.transform.position = Spawnpoint.transform.position;
        }
    }
}
