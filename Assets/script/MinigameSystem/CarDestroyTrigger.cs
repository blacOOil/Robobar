using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyTrigger : MonoBehaviour
{
    public float CheckerRadius = 1f;
    public GameObject Closest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCarClose())
        {
            Destroy(Closest);
        }
    }
    public bool IsCarClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Car"))
            {
                Closest = hitCollider.gameObject;
                return true;

            }
        }
        return false;
    }
}
