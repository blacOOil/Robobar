using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(mainCamera.transform);
        Vector3 directionToTable = (mainCamera.transform.position - transform.position).normalized;

        // Calculate the new forward vector using RotateTowards
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTable, Time.deltaTime * 10000f, 0.0f);

        // Apply the rotation
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}
