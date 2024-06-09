using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qsingle : MonoBehaviour
{
    public bool isCustomerhere;
    // Start is called before the first frame update
    void Start()
    {
        isCustomerhere = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Customer"))
        {
            isCustomerhere = false;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isCustomerhere = true;
        }
    }
}
