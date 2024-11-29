using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSingle : MonoBehaviour
{
    public bool isSited,IsThisSofa;
    public float CustomerCheckerRadius = 1f;
    public LayerMask CustomerLayer;


    // Start is called before the first frame update
    void Start()
    {
        isSited = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IscustomerClose())
        {
            isSited = true;
        }
        else
        {
            isSited = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isSited = true;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            isSited = false;
        }
    }
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
}
