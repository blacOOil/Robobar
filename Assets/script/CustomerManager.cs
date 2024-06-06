using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Transform> WaitingPlace;
    public List<GameObject> Customer;
    // Start is called before the first frame update
    void Start()
    {
        Transform spawnLocation = WaitingPlace[0];
        GameObject customerPrefab = Customer[0];
        Instantiate(customerPrefab, spawnLocation.position, spawnLocation.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
