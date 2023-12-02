using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject firstCame, SecondCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
        SecondCam.gameObject.SetActive(true);
        firstCame.gameObject.SetActive(false);
           
            
        }
        
        
    }
}
