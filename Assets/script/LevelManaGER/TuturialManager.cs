using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuturialManager : MonoBehaviour
{
    public bool Isspeaking;
    public GameObject SpeakingPanel;
    public GameObject Mareindicator1, Mareindicator2;
    public int TutorialStage;
    public List<Sprite> SpeakerImageList;
    public List<GameObject> MovementTutorial, PlayerList;
    public TextMeshProUGUI SpeakerName, SpeakerDialogue;
    public Image SpeakerImage;
    public CharacterSelection characterSelection;
    private bool IsMovepadSPawned = false;
    private GameObject Player1MovePad, Player2MovePad;

    // Start is called before the first frame update
    void Start()
    {
        TutorialStage = 1;
        Mareindicator1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Manage speaking panel visibility
        SpeakingPanel.SetActive(Isspeaking);

        // Handle tutorial stages
        HandleTutorialStages();

        // Ensure pads follow players if they are spawned
        if (IsMovepadSPawned)
        {
            UpdatePadPositions();
        }
    }

    void HandleTutorialStages()
    {
        switch (TutorialStage)
        {
            case 1:
                Isspeaking = true;
                SpeakerImage.sprite = SpeakerImageList[0];
                SpeakerName.text = "Ikarus";
                SpeakerDialogue.text = "Welcome";
                PressEchageStage(2);
                break;

            case 2:
                Isspeaking = false;
                if (characterSelection.IsGamealreadyPlay)
                {
                    TutorialStage = 3;
                }
                break;

            case 3:
                Isspeaking = false;
                FindPlayerlisted();
                SetTutorialPad();
                PressEchageStage(4);
                break;

            case 4:
                Isspeaking = false;
                Mareindicator1.SetActive(true);
                PressEchageStage(5);
                break;

            case 5:
              
                SpeakerImage.sprite = SpeakerImageList[0];
                SpeakerName.text = "Ikarus";
                SpeakerDialogue.text = "Give it to customer";
                Mareindicator1.SetActive(false);
                DestroyPads();
                break;
        }
    }

    void UpdatePadPositions()
    {
        // Make Player1MovePad and Player2MovePad follow PlayerList[0] and PlayerList[1]
        Player1MovePad.transform.position = PlayerList[0].transform.position;
        Player2MovePad.transform.position = PlayerList[1].transform.position;
    }

    public void FindPlayerlisted()
    {
        // Assuming Player1 and Player2 have the correct tags
        PlayerList[0] = GameObject.FindGameObjectWithTag("Player1");
        PlayerList[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    public void SetTutorialPad()
    {
        if (!IsMovepadSPawned)
        {
            // Instantiate movement pads
            Player1MovePad = Instantiate(MovementTutorial[0], PlayerList[0].transform.position, PlayerList[0].transform.rotation);
            Player2MovePad = Instantiate(MovementTutorial[1], PlayerList[1].transform.position, PlayerList[1].transform.rotation);
            IsMovepadSPawned = true;
        }
    }

    public void DestroyPads()
    {
        if (Player1MovePad != null)
        {
            Destroy(Player1MovePad);
        }

        if (Player2MovePad != null)
        {
            Destroy(Player2MovePad);
        }

        IsMovepadSPawned = false; // Reset flag after destruction
    }

    public void PressEchageStage(int index)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TutorialStage = index;
        }
    }
}
