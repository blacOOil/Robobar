using System.Collections;
using UnityEngine;

public class CustomerWelcomeScript : MonoBehaviour
{
    public GameObject customerManagerObject;
    public CustomerManager customerManager;

    private bool playerInCollision = false;
    private bool canSpawnCustomer = true; // Flag to control spawning delay

    void Start()
    {
        customerManagerObject = GameObject.Find("CustomerManager");
        if (customerManagerObject != null)
        {
            customerManager = customerManagerObject.GetComponent<CustomerManager>();
        }
        else
        {
            Debug.LogError("CustomerManagerObject not found");
        }
    }

    void Update()
    {
        if (playerInCollision && Input.GetKeyDown(KeyCode.F) && canSpawnCustomer)
        {
            StartCoroutine(SpawnCustomerWithDelay());
        }
    }

    IEnumerator SpawnCustomerWithDelay()
    {
        canSpawnCustomer = false; // Prevent spawning again until delay is over
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed

        customerManager.SpawnLastPlaceCustomer();

        yield return new WaitForSeconds(1.0f); // Example additional delay after spawning
        canSpawnCustomer = true; // Allow spawning again
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollision = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInCollision = false;
        }
    }
}
