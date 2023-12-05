using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMangement : MonoBehaviour
{
    public GameObject BackLight, BackLight0;
    // Start is called before the first frame update
    void Start()
    {
        BackLight.SetActive(false);
        BackLight0.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            BackLight.SetActive(true);
            BackLight0.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            BackLight.SetActive(false);
            BackLight0.SetActive(false);
        }
       
    }
}
