using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Transform Hand;
    public bool ishandholdedrink, IsreadytoServered,Ishandholdetable;
    public float CustomerCheckerRadius = 1f;
    public LayerMask CustomerLayer;
    public GameObject ClosestCustomer, drinkholding,Tagholding;

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
        Tagholding = null;
        ishandholdedrink = false;
        IsreadytoServered = false;
        Ishandholdetable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishandholdedrink && !IscustomerClose())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseDrink();
            }
        }

        if (ishandholdedrink && IscustomerClose())
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
            ishandholdedrink = false;
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
            if (ishandholdedrink == false)
            {
                TransformObjToHand(collision.gameObject);
                ishandholdedrink = true;
            }
            else
            {

            }

        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tableTag"))
        {
            if (ishandholdedrink == false && Ishandholdetable == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TransformObjToHand(other.gameObject);
                    Ishandholdetable = true;
                }


            }
            else
            {

            }
        }
    }

    private void TransformObjToHand(GameObject Obj)
    {
        // Set the drink's position and parent to the hand
        Obj.transform.SetParent(Hand);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        if (Obj.CompareTag("drink"))
        {
            drinkholding = Obj;
        }
        else if (Obj.CompareTag("tableTag"))
        {
            Tagholding = Obj;
        }
        

        // Freeze the drink's position by setting its Rigidbody to kinematic
        Rigidbody rb = Obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Set the drink's collider to trigger
        Collider collider = Obj.GetComponent<Collider>();
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
        ishandholdedrink = false;
    }

    }