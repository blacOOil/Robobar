using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCoding : MonoBehaviour
{
   public List<GameObject> ReadyButton,Readyindicator;
    public int readyIndicatorCount;
    private int ReadyNum;
    public GameObject PlayButton;
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
        if(ReadyNum == readyIndicatorCount)
        {
            PlayButton.SetActive(true);
        }
    }
    public void ReadyButt(int Num)
    {
        ReadyNum++;
        Readyindicator[Num].SetActive(true);
    }
}
