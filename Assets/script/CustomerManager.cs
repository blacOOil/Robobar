using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Transform> WaitingPlace;
    public List<GameObject> Customer;
    public List<GameObject> SpawnedCustomer;
    public int QueuePlace,CustomerCount = 0;

    public Collider PlayerDetectCollder;

    public bool IScustomerorgnized,IScustomerSpawned;
    


    // Start is called before the first frame update
    void Start()
    {
        QueuePlace = 0;
        //SpawnCustomer();
        IScustomerorgnized = false;
        IScustomerSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CustomerCount <= (WaitingPlace.Count - 1)&& (IScustomerSpawned == false))
        {
            SpawnCustomer();
            QueuePlace++;
            if(QueuePlace == WaitingPlace.Count)
            {
                IScustomerSpawned = true;
            }
            


        }
        else if(IScustomerorgnized == false)
        {
            SpawnLastPlaceCustomer();
            CustomerAliment();
        }
        else
        {

        }
    }

    public void SpawnCustomer()
    {
        Transform spawnLocation = WaitingPlace[QueuePlace];
        GameObject customerPrefab = Customer[Random.Range(0,(Customer.Count))];
        GameObject SpawnedCustomer = Instantiate(customerPrefab, spawnLocation.position, spawnLocation.rotation);
        AddtoSpawnedlist(SpawnedCustomer);
        CustomerCount++;
        IScustomerorgnized = false;
    }
    public void SpawnLastPlaceCustomer()
    {
        Transform spawnLocation = WaitingPlace[WaitingPlace.Count-1];
        GameObject customerPrefab = Customer[Random.Range(0, (Customer.Count))];
        GameObject SpawnedCustomer = Instantiate(customerPrefab, spawnLocation.position, spawnLocation.rotation);
        AddtoSpawnedlist(SpawnedCustomer);
    }
  
    public void AddtoSpawnedlist(GameObject customer)
    {
        SpawnedCustomer.Add(customer);
    }
    public void CustomerAliment()
    {
        int maxCount = Mathf.Min(SpawnedCustomer.Count, WaitingPlace.Count-1);
        for (int i = 1; i < maxCount; i++) 
        {
                if (SpawnedCustomer[i] != null)  
                {
                SpawnedCustomer[i].transform.position = WaitingPlace[i].position;
                }
                else
                {
             
                }
                   
        }
        IScustomerorgnized = true;

    }
   public void RemoveSpawnedList()
    {
        SpawnedCustomer.RemoveAt(0);
        IScustomerorgnized = false;
    }
}


