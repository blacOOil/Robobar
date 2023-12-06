using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1StartCutScene : MonoBehaviour
{
    public GameObject StartingText;
    public float Sceneduration;
    public bool IsCutSceneEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartingText.SetActive(true);    
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCutSceneEnd == false)
        {
            StartingText.SetActive(true);
            StartCoroutine(StartLevel());
        }
        else
        {
            StartingText.SetActive(false);
        }
    }
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(Sceneduration);
        IsCutSceneEnd = true;
    }
}
