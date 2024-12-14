using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScneChanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMainMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMainMenuSound();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayGame(string FinalProjectDemo)
    {
        SceneManager.LoadScene(FinalProjectDemo);
    }

}
