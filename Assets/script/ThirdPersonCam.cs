using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // The target object to follow
    public Vector3 offset; // Offset from the target object
    public float distance = 5.0f; // Distance from the target
    public float height = 2.0f; // Height of the camera
    
    public float rotationSpeed = 5.0f;

    private float currentRotation = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            // Calculate the desired position of the camera
            Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
            Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);
            // Smoothly move the camera towards the desired position
            transform.position = desiredPosition;
            // Make the camera look at the target
            transform.LookAt(target.position);

            // Rotate the camera based on mouse input
            currentRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        }
    }

}
