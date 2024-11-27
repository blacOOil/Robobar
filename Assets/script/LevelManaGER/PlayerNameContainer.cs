using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameContainer : MonoBehaviour
{
   public List<string> PlayerNameList;
   public List<TMP_InputField> PlayerNameInputList;
    // Start is called before the first frame update
    void Start()
    {
        // Try to find the input fields and add them to the list
        TMP_InputField inputField1 = GameObject.Find("InputField1")?.GetComponent<TMP_InputField>();
        TMP_InputField inputField2 = GameObject.Find("InputField2")?.GetComponent<TMP_InputField>();
        TMP_InputField inputField3 = GameObject.Find("InputField3")?.GetComponent<TMP_InputField>();

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NameListContaining()
    {
        // Check if PlayerNameInputList contains enough input fields and if their text is valid (not null or empty)
        if (PlayerNameInputList.Count >= 3)
        {
            // Check each input field for null text and add to list if valid
            if (!string.IsNullOrEmpty(PlayerNameInputList[0].text))
                PlayerNameList.Add(PlayerNameInputList[0].text);
            else
                PlayerNameList.Add("Default Name 1");  // Add a default value if the text is empty or null

            if (!string.IsNullOrEmpty(PlayerNameInputList[1].text))
                PlayerNameList.Add(PlayerNameInputList[1].text);
            else
                PlayerNameList.Add("Default Name 2");  // Add a default value if the text is empty or null

            if (!string.IsNullOrEmpty(PlayerNameInputList[2].text))
                PlayerNameList.Add(PlayerNameInputList[2].text);
            else
                PlayerNameList.Add("Default Name 3");  // Add a default value if the text is empty or null
        }
        else
        {
            Debug.LogWarning("Not all input fields are found or accessible.");
        }
    }
}
