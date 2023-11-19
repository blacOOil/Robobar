using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialoguePresenter : MonoBehaviour
{
    public GameObject DialogueBox;
    bool Istalk = false;
    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Istalk == false)
        {
            DialogueBox.SetActive(false);
        }
        else
        {
            DialogueBox.SetActive(true);
        }
    }
   public void StartTalk()
    {
        Istalk = true;
    }
   public void EndTalk()
    {
        Istalk = false;
    }
}
