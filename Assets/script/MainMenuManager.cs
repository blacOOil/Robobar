using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject playcam, Settingcam, StartCam, PlayPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCam.SetActive(false);
        backbut();
        PlayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void backbut()
    {
        playcam.SetActive(false);
        Settingcam.SetActive(false);
        PlayPanel.SetActive(false);
    }
    public void playbutt()
    {
        playcam.SetActive(true);
    }
    public void Setbutt()
    {
        Settingcam.SetActive(true);
    }
    public void PlaybuttonPress()
    {
        PlayPanel.SetActive(true);
    }
    public void PlaybackbuttonPress()
    {
        PlayPanel.SetActive(false);
    }
}
