using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Transform> WaitingPlace;
    public List<GameObject> Customer;
    
    public int QueuePlace,CustomerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        QueuePlace = 0;
        //SpawnCustomer();

    }

    // Update is called once per frame
    void Update()
    {
        if( CustomerCount <= (WaitingPlace.Count - 1 ))
        {
            SpawnCustomer();
            QueuePlace++;
        }
        else
        {

        }
    }

    public void SpawnCustomer()
    {
        Transform spawnLocation = WaitingPlace[QueuePlace];
        GameObject customerPrefab = Customer[Random.Range(0,(Customer.Count))];
        Instantiate(customerPrefab, spawnLocation.position, spawnLocation.rotation);
        CustomerCount++;
    }


}
