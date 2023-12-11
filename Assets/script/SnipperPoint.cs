using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnipperPoint : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform Wheel;
    private Rigidbody playerRigidbody;
    public float speed;
    public GameObject count1, count2, count3;
    // Start is called before the first frame update
    void Start()
    {
        count1.SetActive(false);
        count2.SetActive(false);
        count3.SetActive(false);
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
        yield return new WaitForSeconds(1);
        count3.SetActive(true);
        yield return new WaitForSeconds(1);
        count3.SetActive(false);
        count2.SetActive(true);
        yield return new WaitForSeconds(1);
        count2.SetActive(false);
        count1.SetActive(true);
        yield return new WaitForSeconds(1);
        count1.SetActive(false);
    }
    void FreezeWheel()
    {
        // Disable motor torque and steering
        wheelCollider.motorTorque = 0f;
        wheelCollider.steerAngle = 0f;

    }
}

