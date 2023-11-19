using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueManager : MonoBehaviour
{
    //public GameObject Talkarea;
    public TextMeshProUGUI nameText,dialogueText;
    //  bool isconversationstart =false;
    

    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        
        sentences = new Queue<string>();
        
    }
     void Update()
    {
       if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            DisplayNextSentence();
        }
    }
    //  void Update()
    //{
    //  if(isconversationstart == false)
    //{
    //Talkarea.SetActive(false);
    // }
    // else
    // {
    //   Talkarea.SetActive(true);
    //}
    //}
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

       public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation");
        FindAnyObjectByType<dialoguePresenter>().EndTalk();

    }
   // public void ConversationTrigger()
    //{
      //  isconversationstart = true;
    //}

   
}
