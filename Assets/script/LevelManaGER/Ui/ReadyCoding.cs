using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCoding : MonoBehaviour
{
    public GameObject playButton;
    public List<GameObject> readyButton, readyIndicator, selectorButton, notReadyIndicator;
    private int readyCount = 0;
    private bool isReadyToPlay = false;
    public List<int> selectedCharacters; // Track selected character IDs for each player
    public List<bool> playerReadyStatus; // Track Ready for each player

    // Start is called before the first frame update
    void Start()
    {
        playButton.SetActive(false);

        if (readyIndicator == null)
        {
            Debug.LogError("ReadyIndicator list is not assigned in the Inspector.");
            return;
        }
        
        for (int i = 0; i < readyIndicator.Count; i++)
        {
            readyIndicator[i].SetActive(false);
        }

        // Initialize the selectedCharacters list
        selectedCharacters = new List<int> { -1, -1, -1 }; // Default -1 for unselected
        playerReadyStatus = new List<bool> { false, false, false }; // All players start as "Unready"

        UpdateReadyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReadyToPlay && readyCount == readyIndicator.Count && AreCharactersUnique())
        {
            playButton.SetActive(true);
            isReadyToPlay = true;
          
        }

        UpdateReadyButtons();
        if (Input.GetButtonDown("Player1Action"))
        {
            ReadyButtonPressed(0);
        }
        if (Input.GetButtonDown("Player2Action"))
        {
            ReadyButtonPressed(1);
        }
        if (Input.GetButtonDown("Player3Action"))
        {
            ReadyButtonPressed(2);
        }
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

        GameObject readyButtons = readyButton[playerNum];
        Button buttonComponent = readyButtons.GetComponent<Button>();
        TMP_Text buttonText = readyButtons.GetComponentInChildren<TMP_Text>();

        if (readyIndicator[playerNum].activeSelf)
        {
            // Unready the player
            readyCount--;
            readyIndicator[playerNum].SetActive(false);
            buttonText.text = "Ready"; // Switch back to "Ready"
            playerReadyStatus[playerNum] = false;
            Debug.Log($"Player {playerNum} is now unready.");
        }
        else
        {
            // Ready the player
            readyCount++;
            readyIndicator[playerNum].SetActive(true);
            buttonText.text = "Unready"; // Switch to "Unready"
          //  playerReadyStatus[playerNum] = true;
            Debug.Log($"Player {playerNum} is ready.");
        }

        Debug.Log($"Player {playerNum} is ready. Total ready players: {readyCount}");
    }

    private void UpdateReadyButtons()
    {
        bool canReady = AreCharactersUnique();

        for (int i = 0; i < readyButton.Count; i++)
        {
            Button buttonComponent = readyButton[i].GetComponent<Button>();
            TMP_Text buttonText = readyButton[i].GetComponentInChildren<TMP_Text>();

            if (buttonComponent == null || buttonText == null)
            {
                Debug.LogError($"Missing components on ReadyButton[{i}].");
                continue;
            }

            // Always show the button but toggle its interactable state
            readyButton[i].SetActive(true);
            buttonComponent.interactable = canReady || readyIndicator[i].activeSelf;
            buttonText.text = readyIndicator[i].activeSelf ? "Unready" : "Ready";

            // Hide selector if player is ready
            selectorButton[i].SetActive(!playerReadyStatus[i]);

            // Disable character selection buttons if ready
            if (readyIndicator[i].activeSelf)
            {
                DisableSelectorButtons(i);
            }
            else
            {
                EnableSelectorButtons(i);
            }

            // Change text color based on button interactability
            if (!buttonComponent.interactable)
            {
                buttonText.color = new Color32(96, 96, 118, 255); // #606076
                if (notReadyIndicator[i] != null)
                {
                    notReadyIndicator[i].SetActive(true); // Show warning image
                }
            }
            else
            {
                buttonText.color = Color.white; // Reset to default color
                if (notReadyIndicator[i] != null)
                {
                    notReadyIndicator[i].SetActive(false); // Show warning image
                }
            }
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

    

    private void DisableSelectorButtons(int playerNum)
    {
        // Disable left and right buttons for this player
        if (selectorButton[playerNum] != null)
        {
            
            selectorButton[playerNum*2].GetComponent<Button>().interactable = false;
            selectorButton[playerNum*2 + 1].GetComponent<Button>().interactable = false;
           
        }
    }

    private void EnableSelectorButtons(int playerNum)
    {
        // Enable left and right buttons for this player
        if (selectorButton[playerNum] != null)
        {
            selectorButton[playerNum * 2].GetComponent<Button>().interactable = true;
            selectorButton[playerNum * 2 + 1].GetComponent<Button>().interactable = true;
        }
    }

 

}
