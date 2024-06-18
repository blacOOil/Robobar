using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingSystem : MonoBehaviour
{
    public GameObject cookingCanvas,MenuCanvas;
    bool IsPlayerClose = false,IsCooking = false;
    public Transform ServiceSpawner;
    public List<GameObject> ListDrink;
   
    // Start is called before the first frame update
    void Start()
    {
        cookingCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
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
        if(IsCooking )
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnDrink(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnDrink(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpawnDrink(2);
            }
        }
    }
    public void SpawnDrink(int index)
    {
        Instantiate(ListDrink[index], ServiceSpawner.position, Quaternion.identity);
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
        if (Input.GetKey(KeyCode.E))
        {
            MenuCanvas.SetActive(true);
            IsCooking = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            MenuCanvas.SetActive(false);
            IsCooking = false;
        }
        
    }

}
