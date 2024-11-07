using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSkill : MonoBehaviour
{
    public ServiceSystem serviceSystem;
    public GameObject[] CollectedItem;
    public Transform CollecterArea;
    // Start is called before the first frame update
    void Start()
    {
      //  CollectedItem = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StorExtradrink(GameObject drink)
    {
        // Check if there is an empty slot in the array
        for (int i = 0; i < CollectedItem.Length; i++)
        {
            if (CollectedItem[i] == null)
            {
                CollectedItem[i] = drink;
               
                TranformDrinktoCollecter(drink);
                Debug.Log("Drink stored at index " + i);
                return;
            }
        }

        Debug.Log("No empty slot available to store the drink.");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drink"))
        {
            if(serviceSystem.Ishandholded == true)
            {
             //   StorExtradrink(collision.gameObject);
            }
        }
    }
    public void TranformDrinktoCollecter(GameObject drink)
    {
        drink.transform.position = CollecterArea.transform.position;
        drink.GetComponent<MeshRenderer>().enabled = false;
    }
}
