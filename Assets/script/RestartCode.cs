using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCode : MonoBehaviour
{
    private bool isReloding = false;
    public float holdDuration;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (!isReloding)
            {
                isReloding = true;
                timer = 0.0f;
            }
            timer += Time.deltaTime;
            if(timer >= holdDuration)
            {
                ReloadCurrentScene();
            }
        }
    }
    void ReloadCurrentScene()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(CurrentSceneIndex);
    }
}
