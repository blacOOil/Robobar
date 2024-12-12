using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noDestroyOnLoad : MonoBehaviour
{
    public GameObject settingPanel,SettingCanvas;
    public static noDestroyOnLoad instance;
    
    void Start() 
    {
       
        // Set this instance as the singleton
        instance = this;

        // Ensure DontDestroyOnLoad is called before any potential destruction
        DontDestroyOnLoad(gameObject);
        // Check if an instance already exists
        Debug.Log("DontDestroyOnLoad: Object marked as persistent.");
      

     

        
    }



    void Update() {
        if (SettingCanvas == null)
        {
            SettingCanvas = GameObject.Find("MainCanvas");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingPanel != null)
            {

                    GameObject newObject = Instantiate(settingPanel, Vector3.zero, Quaternion.identity);
                    newObject.transform.SetParent(SettingCanvas.transform);
                newObject.transform.localPosition = new Vector3(100, 50, 0); // Example position
                newObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Example scale

            }
            else
            {
                Debug.LogWarning("Setting panel is not assigned in the Inspector!");
            }
        }
    }
}
