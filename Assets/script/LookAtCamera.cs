using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject[] Ui;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ui[0].transform.LookAt(mainCamera.transform);
        Ui[1].transform.LookAt(mainCamera.transform);
    }
}
