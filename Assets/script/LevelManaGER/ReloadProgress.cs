using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadProgress : MonoBehaviour
{
    public Slider Slider;
    
    

    public float Fillspeed;
    
    // Start is called before the first frame update

    private void Awake()
    {
        Slider = gameObject.GetComponent<Slider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Slider.value += Fillspeed * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
                 if(Slider.value <= 0) 
                { 
                Slider.value -= Fillspeed * Time.deltaTime;
                }
        }
    }
   
}
