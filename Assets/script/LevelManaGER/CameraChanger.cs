using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject firstCame, SecondCam;
    // Start is called before the first frame update
    void Start()
    {
        SecondCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        firstCame.gameObject.SetActive(true);
        SecondCam.gameObject.SetActive(false);
    }
}
