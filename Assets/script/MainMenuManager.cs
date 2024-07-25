using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [Header("Camera")]
    public GameObject StartCam;
    public GameObject PlayCam, SettingCam, MultiSelectedCamPos;

    [Header("UI-Canvas")]
    public GameObject PlaySelection;
    public GameObject MultiSelected;

    void Start() {
        StartCam.SetActive(false);
        
        WhenPressBack();
    }

    // Update is called once per frame
    void Update() {

    }

    public void WhenPressBack() {
        PlayCam.SetActive(false);
        PlaySelection.SetActive(false);

        SettingCam.SetActive(false);

        MultiSelectedCamPos.SetActive(false);
        MultiSelected.SetActive(false);
    }

    public void WhenPressPlay() {
        PlayCam.SetActive(true);
        PlaySelection.SetActive(true);

        MultiSelectedCamPos.SetActive(false);
    }

    public void WhenPressSetting() {
        SettingCam.SetActive(true);
    }

    public void WhenPressMulti() {
        MultiSelectedCamPos.SetActive(true);
        MultiSelected.SetActive(true);
    }
}
