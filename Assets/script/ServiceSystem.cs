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
                ClosestCustomer = hitCollider.gameObject;
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
            IsreadytoServered = true;
        }
        else
        {
            IsreadytoServered = false;
        }
        if (IsreadytoServered == true && ClosestCustomer == null && ClosestCustomer.GetComponent<CustomerSingle>().Randomdrinkfloat == drinkholding.GetComponent<DrinkSingle>().DrinkId)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ServiceProceed();
            }
            
            
        }
        else
        {

        }
    }
    public void ServiceProceed()
    {
       
            Debug.Log("Id Match");
            drinkholding.GetComponent<DrinkSingle>().selfDestruct();
            ishandholded = false;
            drinkholding = null;
        
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