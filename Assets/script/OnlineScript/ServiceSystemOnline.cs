using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  // Include Photon PUN namespace
using Photon.Realtime;

public class ServiceSystemOnline : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Transform Hand;
    public bool holdedrink, IsreadytoServered, holdetable, IsTableTagnear, Ishandholded;
    public float CustomerCheckerRadius = 1f;
    public LayerMask CustomerLayer;
    public GameObject ClosestCustomer, Objholding, ReadytoPickObj;

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
        Objholding = null;
        holdedrink = false;
        IsreadytoServered = false;
        holdetable = false;
        IsTableTagnear = false;
        Ishandholded = false;
        monneyLevelCode = FindObjectOfType<MonneyLevelCode>();
    }

    // Update is called once per frame
    void Update()
    {

        if ((holdedrink) && !IscustomerClose())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseDrink();
            }
        }
        if (holdetable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseDrink();
            }
        }

        if (holdedrink && IscustomerClose())
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
        if (holdedrink == false && IsTableTagnear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TransformObjToHand(ReadytoPickObj);
            }

        }

        if (Ishandholded == false)
        {
            holdedrink = false;
            holdetable = false;
        }
    }
    public void ServiceProceed()
    {
        monneyLevelCode.moneyAdd();
        findClosestCustomer();
        if (holdedrink == true)
        {
            if (ClosestCustomer != null && ClosestCustomer.GetComponent<CustomerSingle>().Randomdrinkfloat == Objholding.GetComponent<DrinkSingle>().DrinkId)
            {
                Debug.Log("ServiceProceed");
                Objholding.GetComponent<DrinkSingle>().selfDestruct();
                holdedrink = false;
                Ishandholded = false;
                Objholding = null;
                // Destroy(Objholding);
            }

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
            if (Ishandholded == false)
            {
                TransformObjToHand(collision.gameObject);
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
            if (Ishandholded == false)
            {
                IsTableTagnear = true;
                ReadytoPickObj = other.gameObject;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tableTag"))
        {
            IsTableTagnear = false;

        }
    }

    private void TransformObjToHand(GameObject Obj)
    {
        // Set the drink's position and parent to the hand
        Obj.transform.SetParent(Hand);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        Objholding = Obj;
        if (Obj.CompareTag("tableTag"))
        {
            holdetable = true;
        }
        if (Obj.CompareTag("drink"))
        {
            holdedrink = true;
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
        Ishandholded = true;
    }
    public void ReleaseDrink()
    {
        Objholding.GetComponent<Rigidbody>().isKinematic = false;
        if (Objholding.CompareTag("drink"))
        {
            Objholding.transform.SetParent(null);
            Objholding.GetComponent<Collider>().isTrigger = false;
        }
        else if (Objholding.CompareTag("tableTag"))
        {
            GameObject tabletagPlace = Objholding.GetComponent<SeatSetSingle>().TableTagTranform;
            Objholding.transform.position = tabletagPlace.transform.position;
            Objholding.transform.SetParent(tabletagPlace.transform);
            Objholding.GetComponent<Collider>().isTrigger = true;
        }
        StartCoroutine(DropDelay());

    }
    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(1f);
        Ishandholded = false;
        Objholding = null;
    }

}
