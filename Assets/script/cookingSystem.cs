using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingSystem : MonoBehaviour
{
    public GameObject cookingCanvas;
    bool IsPlayerClose = false;
    // Start is called before the first frame update
    void Start()
    {
        cookingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerClose == true)
        {
            PlayerISClose();
        }
        else
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if( other.CompareTag("Player")){
        IsPlayerClose = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        IsPlayerClose = false;
        }
        
    }
    public void PlayerISClose()
    {
        cookingCanvas.SetActive(true);
    }
}
