using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject settingPanel, 
                      settingCanvas;
    public GameObject[] musicObj;
    public static DontDestroyOnLoad instance;
    
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
        if (settingCanvas == null)
        {
            settingCanvas = GameObject.Find("MainCanvas");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingPanel != null)
            {

                    GameObject newObject = Instantiate(settingPanel, Vector3.zero, Quaternion.identity);
                    newObject.transform.SetParent(settingCanvas.transform);
                newObject.transform.localPosition = new Vector3(100, 50, 0); // Example position
                newObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Example scale

            }
            else
            {
                Debug.LogWarning("Setting panel is not assigned in the Inspector!");
            }
        }
    }

    private void Awake() {
        musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // void SetupMusic() {
    //     if(FindObjectOfType(GetType()).Length > 1) {

    //     }
    // }
}
