using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSkill : MonoBehaviour
{
    public List<CustomerSingle> CustomerSingleList;
    public float CheckerRadiuz = 100f;
    public LayerMask customerLayer;

    // Start is called before the first frame update
    void Start()
    {
        CustomerSingleList = new List<CustomerSingle>();
    }

    // Update is called once per frame
    void Update()
    {
        FindCustomersInArea();


    }
    public void FindCustomersInArea()
    {
        // Use Physics.OverlapSphere to find all colliders within the radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, CheckerRadiuz, customerLayer);
        HashSet<CustomerSingle> currentCustomers = new HashSet<CustomerSingle>();

        // Add all customers found within the radius to the hash set
        foreach (Collider collider in colliders)
        {
            CustomerSingle customer = collider.GetComponent<CustomerSingle>();
            if (customer != null)
            {
                currentCustomers.Add(customer);
            }
        }

        // Remove customers that are no longer within the radius
        for (int i = CustomerSingleList.Count - 1; i >= 0; i--)
        {
            if (!currentCustomers.Contains(CustomerSingleList[i]))
            {
                CustomerSingleList.RemoveAt(i);
                Debug.Log("Customer removed from the list");
            }
        }

        // Add new customers that are not already in the list
        foreach (CustomerSingle customer in currentCustomers)
        {
            if (!CustomerSingleList.Contains(customer))
            {
                if(customer.IsDanceAdded == false)
                {
                    customer.RemainmingTime += 5;
                    customer.IsDanceAdded = true;
                }
                
                CustomerSingleList.Add(customer);
                Debug.Log("New customer added to the list");
            }
        }
    }
}
