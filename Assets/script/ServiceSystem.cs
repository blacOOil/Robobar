using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public Transform Hand;
    public bool ishandholded;
    // Start is called before the first frame update
    void Start()
    {
        ishandholded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drink"))
        {
            TransformDrinkToHand(collision.gameObject);
            ishandholded = true;
        }
    }

    private void TransformDrinkToHand(GameObject drink)
    {
        // Set the drink's position and parent to the hand
        drink.transform.SetParent(Hand);
        drink.transform.localPosition = Vector3.zero;
        drink.transform.localRotation = Quaternion.identity;

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