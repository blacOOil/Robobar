using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuturialManager : MonoBehaviour
{
    public bool Isspeaking;
    public GameObject SpeakingPanel;
    public GameObject Mareindicator1,Mareindicator2;
    public int TutorialStage;
    public List<Sprite> SpeakerImageList;
    public TextMeshProUGUI SpeakerName, SpeakerDialogue;
    public Image SpeakerImage;

    // Start is called before the first frame update
    void Start()
    {
        TutorialStage = 1;
        Mareindicator1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Isspeaking == true)
        {
            SpeakingPanel.SetActive(true);
        }
        else
        {
            SpeakingPanel.SetActive(false);
        }

        if(TutorialStage == 1)
        {
            Isspeaking = true;
            SpeakerImage.sprite = SpeakerImageList[0];
            SpeakerName.text = "Ikarus";
            SpeakerDialogue.text = "Welcome";
            PressEchageStage(2);
        }
        if(TutorialStage == 2)
        {
            Isspeaking = false;
        }
        if(TutorialStage == 3)
        {
            Isspeaking = true;
            SpeakerImage.sprite = SpeakerImageList[0];
            SpeakerName.text = "Ikarus";
            SpeakerDialogue.text = "Welcome";
            PressEchageStage(4);
        }
        if(TutorialStage == 4)
        {
            Isspeaking = false;
            Mareindicator1.SetActive(true);
            PressEchageStage(5);
        }
        if(TutorialStage == 5)
        {
            Isspeaking = true;
            SpeakerImage.sprite = SpeakerImageList[0];
            SpeakerName.text = "Ikarus";
            SpeakerDialogue.text = "Give it to customer";
            Mareindicator1.SetActive(false);
        }
        
        

    }
    public void StageChange()
    {
        TutorialStage++;
    }
    public void PressEchageStage(int index)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TutorialStage = index;
        }
    }
}
