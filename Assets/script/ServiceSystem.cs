using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Transform Hand;
    public bool ishandholded, IsreadytoServered;
    public float CustomerCheckerRadius = 1f;
    public LayerMask CustomerLayer;
    public GameObject ClosestCustomer, drinkholding;

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
    // Start is called before the first frame update
    void Start()
    {
        ishandholded = false;
        IsreadytoServered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishandholded && !IscustomerClose())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseDrink();
            }
        }

        if (ishandholded && IscustomerClose())
        {
            IsreadytoServered = true;
        }
        else
        {
            IsreadytoServered = false;
        }

        if (IsreadytoServered)
        {
            findClosestCustomer(); // Ensure this is called to find the closest customer

            if (Input.GetKeyDown(KeyCode.E))
            {
                ServiceProceed();
            }
        }
    }
    public void ServiceProceed()
    {
        monneyLevelCode.moneyAdd();
        findClosestCustomer();
        if (ClosestCustomer != null && ClosestCustomer.GetComponent<CustomerSingle>().Randomdrinkfloat == drinkholding.GetComponent<DrinkSingle>().DrinkId)
        {
            Debug.Log("ServiceProceed");
           drinkholding.GetComponent<DrinkSingle>().selfDestruct();
            ishandholded = false;
            drinkholding = null;
           // Destroy(drinkholding);
        }
        else
        {

        }
    }
    public void findClosestCustomer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestCus = null;

        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            CustomerSingle customerSingle = customer.GetComponent<CustomerSingle>();
            if (customerSingle != null && customerSingle.Isordered)
            {
                float distance = Vector3.Distance(transform.position, customer.transform.position);
                if (distance < closestDistance && distance <= CustomerCheckerRadius)
                {
                    closestDistance = distance;
                    closestCus = customer;
                }
            }
        }

        ClosestCustomer = closestCus;

        if (ClosestCustomer != null)
        {
            Debug.Log("Closest customer found: " + ClosestCustomer.name);
        }
        else
        {
            Debug.Log("No close customer found.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drink"))
        {
            if (ishandholded == false)
            {
                TransformDrinkToHand(collision.gameObject);
                ishandholded = true;
            }
            else
            {

            }

        }
    }

    private void TransformDrinkToHand(GameObject drink)
    {
        // Set the drink's position and parent to the hand
        drink.transform.SetParent(Hand);
        drink.transform.localPosition = Vector3.zero;
        drink.transform.localRotation = Quaternion.identity;
        drinkholding = drink;

        // Freeze the drink's position by setting its Rigidbody to kinematic
        Rigidbody rb = drink.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Set the drink's collider to trigger
        Collider collider = drink.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }
    public void ReleaseDrink()
    {
        drinkholding.transform.SetParent(null);
        drinkholding.GetComponent<Rigidbody>().isKinematic = false;
        drinkholding.GetComponent<Collider>().isTrigger = false;
        StartCoroutine(DropDelay());
        
    }
    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(1f);
        ishandholded = false;
    }

    }