using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipperPoint : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform Wheel;
    private Rigidbody playerRigidbody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
        playerRigidbody = Wheel.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == Wheel.position)
        {
            Shooting();
        }
        transform.position = Vector3.Lerp(transform.position,Wheel.position, speed * Time.deltaTime);
    }
    void Shooting()
    {
        StartCoroutine(Scoping());
        playerRigidbody.isKinematic = true;
        FreezeWheel();

    }

    IEnumerator Scoping()
    {
        yield return new WaitForSeconds(2);
    }
    void FreezeWheel()
    {
        // Disable motor torque and steering
        wheelCollider.motorTorque = 0f;
        wheelCollider.steerAngle = 0f;

    }
}

