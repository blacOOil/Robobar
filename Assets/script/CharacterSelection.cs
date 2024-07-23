using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timemanager;

public class CharacterSelection : MonoBehaviour
{
    public TimerCode timerCode;
    public MonneyLevelCode monneyLevelCode;
    public bool isCharacterSelected;
    public GameObject CharSelectedZone,MainGameUi,SelectingUi,CharacterSelector;
    public List<GameObject> CharacterList;

    public List<Transform> CharacterTranformList;
    public int selectorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        isCharacterSelected = false;
        timerCode.enabled = false;
        monneyLevelCode.enabled = false;
        MainGameUi.SetActive(false);

        foreach(GameObject character in CharacterList)
        {
            CharacterTranformList.Add(character.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharacterSelected)
        {
            timerCode.enabled = true;
            monneyLevelCode.enabled = true;
            MainGameUi.SetActive(true);
            CharSelectedZone.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isCharacterSelected = true;
        }
        if (!isCharacterSelected)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                selectorNumber++;
                SelectorMove();
            }
            if ( selectorNumber >= 0 &&(Input.GetKeyDown(KeyCode.A)))
            {
                selectorNumber--;
                SelectorMove();
            }
        }
    }
    public void SelectorMove()
    {
       
       
        if(selectorNumber >= 0 && selectorNumber < CharacterTranformList.Count)
        {
            CharacterSelector.transform.position = CharacterTranformList[selectorNumber].position;
        }
    }
}
