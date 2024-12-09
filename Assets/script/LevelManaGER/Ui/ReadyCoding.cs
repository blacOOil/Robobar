using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCoding : MonoBehaviour
{
    public GameObject playButton;
    public List<GameObject> readyButton, readyIndicator, selectorButton;
    private int readyCount = 0;
    private bool isReadyToPlay = false;
    public List<int> selectedCharacters; // Track selected character IDs for each player

    // Start is called before the first frame update
    void Start()
    {
        playButton.SetActive(false);
        for (int i = 0; i < readyIndicator.Count; i++)
        {
            readyIndicator[i].SetActive(false);
        }

        // Initialize the selectedCharacters list
        selectedCharacters = new List<int> { -1, -1, -1 }; // Default -1 for unselected
        UpdateReadyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReadyToPlay && readyCount == readyIndicator.Count && AreCharactersUnique())
        {
            playButton.SetActive(true);
            isReadyToPlay = true;
            HandleReadyToPlay();
        }

        UpdateReadyButtons();
    }

    public void ReadyButt(int Num)
    {
        readyIndicator[Num].SetActive(true);
        readyButton[Num].SetActive(false);
    }

    public void CharacterSelected(int playerNum, int characterID)
    {
        if (playerNum < 0 || playerNum >= selectedCharacters.Count)
        {
            Debug.LogError($"Invalid player number: {playerNum}");
            return;
        }

        selectedCharacters[playerNum] = characterID;
        Debug.Log($"Player {playerNum} selected character {characterID}");
        UpdateReadyButtons();
    }

    public void ReadyButtonPressed(int playerNum)
    {
        if (!AreCharactersUnique())
        {
            Debug.Log("Characters must be unique.");
            return;
        }

        readyCount++;
        readyIndicator[playerNum].SetActive(true);
        readyButton[playerNum].SetActive(false); // Make the button inactive

        Debug.Log($"Player {playerNum} is ready. Total ready players: {readyCount}");
    }

    private void UpdateReadyButtons()
    {
        bool canReady = AreCharactersUnique();

        for (int i = 0; i < readyButton.Count; i++)
        {
            // Keep the button visible but enable/disable based on conditions
            readyButton[i].SetActive(true);
            readyButton[i].GetComponent<UnityEngine.UI.Button>().interactable = canReady && !readyIndicator[i].activeSelf;
        }


    }

    private bool AreCharactersUnique()
    {
        HashSet<int> uniqueSelections = new HashSet<int>();

        foreach (int characterID in selectedCharacters)
        {
            if (characterID == -1) return false; // Ensure all characters are selected
            if (!uniqueSelections.Add(characterID)) return false; // Ensure no duplicates
        }

        return true;
    }

    public void HandleReadyToPlay()
    {
        foreach (GameObject button in selectorButton)
        {
            button.SetActive(false);
        }
    }
    public void HidSelector(int num)
    {      
      selectorButton[num].SetActive(false);
      selectorButton[num+1].SetActive(false);
    }
}
