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
        // Clear the list to avoid duplicates
        CustomerSingleList.Clear();

        // Use Physics.OverlapSphere to find all colliders within the radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, CheckerRadiuz, customerLayer);

        foreach (Collider collider in colliders)
        {
            CustomerSingle customer = collider.GetComponent<CustomerSingle>();
            if (customer != null)
            {
                customer.RemainmingTime += 5;
                CustomerSingleList.Add(customer);
               
            }
        }
    }
}
