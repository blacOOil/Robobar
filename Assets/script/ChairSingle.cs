using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSingle : MonoBehaviour
{
    public bool isSited;
    // Start is called before the first frame update
    void Start()
    {
        isSited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Customer"))
    //     {
    //         isSited = false;

    //     }
    // }
    // public void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Customer"))
    //     {
    //         isSited = true;
    //     }
    // }
}
