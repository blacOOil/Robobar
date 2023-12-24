using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject PlayCam, OptionCam,MainCam;
    public GameObject PlayScreen, MainMenuScreen,OptionSceen;
    // Start is called before the first frame update
    void Start()
    {
        MainCam.SetActive(true);
        PlayCam.SetActive(false);
        OptionCam.SetActive(false);
        OptionSceen.SetActive(false);
        PlayScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPlayScreen()
    {
        MainCam.SetActive(false);
        PlayCam.SetActive(true);
        MainMenuScreen.SetActive(false);
        PlayScreen.SetActive(true);
    }
    public void BackPress()
    {
        MainCam.SetActive(true);
        PlayCam.SetActive(false);
        OptionCam.SetActive(false);
        MainMenuScreen.SetActive(true);
        PlayScreen.SetActive(false);
        OptionSceen.SetActive(false);
    }
    public void OptionMenuPress()
    {
        MainCam.SetActive(false);
        OptionCam.SetActive(true);
        MainMenuScreen.SetActive(false);
        OptionSceen.SetActive(true);
    }
}
