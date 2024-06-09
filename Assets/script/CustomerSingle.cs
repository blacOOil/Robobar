using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSingle : MonoBehaviour
{
    public bool Issited,Isfull;
    public GameObject Requested;

    // Start is called before the first frame update
    void Start()
    {
        Requested.SetActive(false);
        Issited = false;
        Isfull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Issited)
        {
            Requested.SetActive(true);
            if(Isfull)
            {
               
            }
        }
        else
        {
            Requested.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chairs"))
        {
            Issited = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Chairs")) 
        {
            Issited = false;
        }
    }


}
