using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playcam, Settingcam,StartCam;
    // Start is called before the first frame update
    void Start()
    {
        StartCam.SetActive(false);
        backbut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void backbut()
    {
        playcam.SetActive(false);
        Settingcam.SetActive(false);
    }
    public void playbutt()
    {
        playcam.SetActive(true);
    }
    public void Setbutt()
    {
        Settingcam.SetActive(true);
    }
}
