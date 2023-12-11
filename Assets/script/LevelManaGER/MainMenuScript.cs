using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject PlayScreen, MainMenuScreen,OptionSceen;
    // Start is called before the first frame update
    void Start()
    {
        OptionSceen.SetActive(false);
        PlayScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPlayScreen()
    {
        MainMenuScreen.SetActive(false);
        PlayScreen.SetActive(true);
    }
    public void BackPress()
    {
        MainMenuScreen.SetActive(true);
        PlayScreen.SetActive(false);
        OptionSceen.SetActive(false);
    }
    public void OptionMenuPress()
    {
        MainMenuScreen.SetActive(false);
        OptionSceen.SetActive(true);
    }
}
