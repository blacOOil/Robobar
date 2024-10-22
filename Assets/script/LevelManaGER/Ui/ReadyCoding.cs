using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCoding : MonoBehaviour
{
   public List<GameObject> ReadyButton,Readyindicator,selectorbutt;
    public int readyIndicatorCount;
    private int ReadyNum;
    public GameObject PlayButton;
    public bool isreadytoplay;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.SetActive(false);
        for (int i = 0; i < Readyindicator.Count; i++)
        {
            Readyindicator[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(ReadyNum == Readyindicator.Count)
        {
            PlayButton.SetActive(true);
            isreadytoplay = true;
        }
        if(isreadytoplay == true)
        {
            Handlereadytoplay();
        }
    }
    public void ReadyButt(int Num)
    {
        ReadyNum++;
        Readyindicator[Num].SetActive(true);
        ReadyButton[Num].SetActive(false);
    }

    public void Handlereadytoplay()
    {
        foreach (GameObject obj in selectorbutt)
        {
            obj.SetActive(false);
        }
    }
    public void HidSelector(int num)
    {      
      selectorbutt[num].SetActive(false);
      selectorbutt[num+1].SetActive(false);
    }
}
