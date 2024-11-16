using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSkill : MonoBehaviour
{
    public ServiceSystem serviceSystem;
    public List<GameObject> CollectedItemSpace;
    public List<int> CollectedId;
    public Transform CollecterArea;
    public DrinkSingle drinkSingle;
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
    
}
