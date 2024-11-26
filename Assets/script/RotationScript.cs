using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;

    public float MinSpeed => minSpeed; // Expose MinSpeed for TimerCode
    public float RotationSpeed => rotationSpeed; // Expose RotationSpeed for TimerCode

    void Start() {
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void SetMinSpeed(float value) {
        minSpeed = value;
    }

    public void SetMaxSpeed(float value) {
        maxSpeed = value;
    }

    public void SetRotationSpeed(float value) {
        rotationSpeed = value;
    }

    void Update() {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
