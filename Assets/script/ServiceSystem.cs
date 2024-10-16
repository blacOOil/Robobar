using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Transform Hand;
    public bool holdedrink, IsreadytoServered, holdetable, IsTableTagnear, Ishandholded;
    private float CustomerCheckerRadius = 2f;
    private int playernumber = 0;
    public LayerMask CustomerLayer,TagLayer;
    public GameObject ClosestCustomer, Objholding,ReadytoPickObj,ClosestTage;

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
    private bool IsTageClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CustomerCheckerRadius, TagLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("tableTag"))
            {
                if (!Ishandholded)
                {
                    ReadytoPickObj = hitCollider.gameObject; // Store the table tag object
                }
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

        if(gameObject.tag == "Player2")
        {
            playernumber = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTageClose()&& Ishandholded == false)
        {  
                IsTableTagnear = true;
        }
        else
        {
            IsTableTagnear = false;
        }
        if ((holdedrink) && !IscustomerClose())
        {
            if (PlayerInput(playernumber))
            {
                ReleaseDrink();
            }
        }
        if(holdetable)
        {
            if (PlayerInput(playernumber))
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

            if (PlayerInput(playernumber))
            {
                ServiceProceed();
            }
        }
        if (holdedrink == false && IsTableTagnear)
        {
            if (PlayerInput(playernumber))
            {
            TransformObjToHand(ReadytoPickObj);
            }

        }
       
       
        
        if(Ishandholded == false)
        {
            holdedrink = false;
            holdetable = false;
        }
    }
    public void ServiceProceed()
    {
        monneyLevelCode.moneyAdd();
        findClosestCustomer();
        if(holdedrink == true)
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
        if (Obj == null)
        {

        }
        {

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
        else if(Objholding.CompareTag("tableTag"))
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
    public bool PlayerInput(int playernum)
    {
        if(playernum == 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                return true;
            }
        }
        if(playernum == 1)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                return true;
            }
        }
        return false;
    }
}
   