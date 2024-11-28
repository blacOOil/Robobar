using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameContainer : MonoBehaviour
{
   public List<string> PlayerNameList;
   public List<TMP_InputField> PlayerNameInputList;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = gameObject.GetComponent<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void NameListContaining()
    {

        // Accessing player names from the ScoreManager
        string playerName1 = scoreManager.PlayerName1;
        string playerName2 = scoreManager.PlayerName2;
        string playerName3 = scoreManager.PlayerName3;

        // Now you can add these to the PlayerNameList or do something else
        PlayerNameList.Clear();  // Clear the list first if necessary
        PlayerNameList.Add(playerName1);
        PlayerNameList.Add(playerName2);
        PlayerNameList.Add(playerName3);

        // Optionally, you can populate the input fields with these names if needed
        PlayerNameInputList[0].text = playerName1;
        PlayerNameInputList[1].text = playerName2;
        PlayerNameInputList[2].text = playerName3;
    }
}
