using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkButtonSingle : MonoBehaviour
{
    public cookingSystem cookingSystem;
    public Image ButtonImage;
    public int ButtonCode,DrinkIndex;

    public Sprite normalSprite;         // Default sprite (not selected)
    public Sprite selectedSprite;       // Sprite when selected
    public Sprite cancelSprite;

    // Start is called before the first frame update
    void Start()
    {
        cookingSystem = GameObject.Find("BarCollider").GetComponent<cookingSystem>();

        ButtonImage.sprite = normalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        DrinkIndex = cookingSystem.DrinkIndex;

        if(ButtonCode == DrinkIndex)
        {
            ButtonImage.sprite = selectedSprite; // Use the selected sprite

            if(gameObject.name == "CancelButton")
            {
                ButtonImage.sprite = cancelSprite; // Use the cancel sprite
            }
        }
        else
        {
            ButtonImage.sprite = normalSprite;
        }
    }
   
}
