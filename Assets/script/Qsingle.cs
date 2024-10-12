using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qsingle : MonoBehaviour
{
    public List<GameObject> Customer;
    public bool isCustomerhere;
    public Transform spawnPoint;
    public float waitTime = 1f;
    private bool isSpawning = false; // Flag to check if the coroutine is running
    public float CustomerCheckerRadius = 1f;
    public LayerMask CustomerLayer;

    IEnumerator StartCustomerSpawning()
    {
        isSpawning = true;
        yield return new WaitForSeconds(waitTime);
        SpawnedSingleCustomer();
        isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isCustomerhere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IscustomerClose())
        {
            isCustomerhere = true;
        }
        else
        {
            isCustomerhere = false;
        }
        if (!isCustomerhere && !isSpawning)
        {
            StartCoroutine(StartCustomerSpawning());
        }
        
    }

    public void SpawnedSingleCustomer()
    {
        if (Customer.Count > 0)
        {
            int randomIndex = Random.Range(0, Customer.Count);
            GameObject randomCustomer = Customer[randomIndex];
            Instantiate(randomCustomer, spawnPoint.position, spawnPoint.rotation);
            isCustomerhere = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isCustomerhere = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isCustomerhere = false;
        }
    }
    private bool IscustomerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CustomerCheckerRadius, CustomerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Customer"))
            {
               
                return true;

            }
        }
        return false;
        
    }
}