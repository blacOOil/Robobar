using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [Header("Camera")]
    public GameObject StartCam;
    public GameObject PlayCam, SettingCam, MultiSelectedCamPos;

    [Header("UI-Canvas")]
    public GameObject PlaySelection;
    public GameObject SingleSelected, MultiSelected;

    void Start() {
        StartCam.SetActive(false);
        SettingCam.SetActive(false);
        
        WhenPressBack();
    }

    // Update is called once per frame
    void Update() {

    }

    public void WhenPressBack() {
        PlayCam.SetActive(false);
        PlaySelection.SetActive(false);

        SettingCam.SetActive(false);

        GameModeSettings(false);
    }

    public void WhenPressPlay() {
        PlayCam.SetActive(true);
        PlaySelection.SetActive(true);

        GameModeSettings(false);
    }

    public void WhenPressSetting() {
        SettingCam.SetActive(true);
    }

    public void WhenPressSingle() {
        MultiSelectedCamPos.SetActive(true);
        SingleSelected.SetActive(true);
    }

    public void WhenPressMulti() {
        MultiSelectedCamPos.SetActive(true);
        MultiSelected.SetActive(true);
    }

    public bool GameModeSettings(bool value) {
        MultiSelectedCamPos.SetActive(value);
        MultiSelected.SetActive(value);
        SingleSelected.SetActive(value);

        return value;
    }

    public void Exit() {
        Application.Quit();
    }
}
