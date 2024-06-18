using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulticameraSystem : MonoBehaviour
{
    public GameObject PlayerCam;
    bool IsPlayerCamUsed ;
    
    // Start is called before the first frame update
    void Start()
    {
        
        IsPlayerCamUsed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerCamUsed)
        {
            PlayerCam.SetActive(true);
            if (Input.GetKeyDown(KeyCode.V))
                {
                IsPlayerCamUsed = false;
               
                }
        }
        else
        {
            PlayerCam.SetActive(false);
            if (Input.GetKeyDown(KeyCode.V))
            {
                IsPlayerCamUsed = true;
            }
        }
        
        
    }
}
