using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
   
    public Transform Hand;
    public bool ishandholded,IsreadytoServered;
    public float CustomerCheckerRadius = 100f;
    public LayerMask CustomerLayer;
    public GameObject ClosestCustomer,drinkholding;
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
        if(ishandholded == true && IscustomerClose() )
        {
            findClosestCustomer();
            IsreadytoServered = true;
        }
        else
        {
            IsreadytoServered = false;
        }
        if (IsreadytoServered == true)
        {
            if (Input.GetKeyDown(KeyCode.E)){
            
            ServiceProceed();
            }
            
        }
    }
    public void ServiceProceed()
    {
        if (ClosestCustomer != null && ClosestCustomer.GetComponent<CustomerSingle>().Randomdrinkfloat == drinkholding.GetComponent<DrinkSingle>().DrinkId)
        {
            drinkholding.GetComponent<DrinkSingle>().selfDestruct();
            ishandholded = false;
            drinkholding = null;
        }
        else
        {

        }
    }
    public void findClosestCustomer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject ClosestCus= null;

        GameObject[] Customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject Customer in Customers)
        {
            CustomerSingle customerSingle = Customer.GetComponent<CustomerSingle>();
            if (customerSingle != null && customerSingle.Isordered)
            {
                float distance = Vector3.Distance(transform.position, Customer.transform.position);
                if (distance < closestDistance && distance <= CustomerCheckerRadius)
                {
                    closestDistance = distance;
                    ClosestCus = Customer;
                   ClosestCustomer = ClosestCus;
                }

            }
            else
            {
                ClosestCustomer = null;
            }
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
   
}