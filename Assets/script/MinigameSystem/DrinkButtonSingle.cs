using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkButtonSingle : MonoBehaviour
{
    public cookingSystem cookingSystem;
    public Image ButtonImage;
    public int ButtonCode,DrinkIndex;
    // Start is called before the first frame update
    void Start()
    {
        cookingSystem = GameObject.Find("BarCollider").GetComponent<cookingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        DrinkIndex = cookingSystem.DrinkIndex;
        if(ButtonCode == DrinkIndex)
        {
            ButtonImage.color = Color.blue;
            if(gameObject.name == "CancelButton")
            {
                ButtonImage.color = Color.red;
            }
        }
        else
        {
            ButtonImage.color = Color.white;
        }
    }
   
}
