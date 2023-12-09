using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenuOption : MonoBehaviour
{
    public GameObject OPtionMenu;
    // Start is called before the first frame update
    void Start()
    {
        OPtionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptionCllicked()
    {
        OPtionMenu.SetActive(true);
        pause();
    }
    void pause()
    {
        Time.timeScale = 0f;
    }
    void Unpause()
    {
        Time.timeScale = 1f;
    }
    public void HideOptionClicked()
    {
        OPtionMenu.SetActive(false);
        Unpause();
    }
}
