using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Rotation speed in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y axis (upwards) at the specified speed
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
