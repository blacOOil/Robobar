using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuturialManager : MonoBehaviour
{
    public Gamestate gamestate;
    public int Tutorial_Stage = 0;
    public List<GameObject> TutorialPanelist;
    public List<GameObject> GameObjTurtorial;
    private bool isConditionMet = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTutorialStage();
    }

    // Update is called once per frame
    void Update()
    {
        CheckConditionsForCurrentStage();
       
    }
  
    public void UpdateTutorialStage()
    {
        for (int i = 0; i < TutorialPanelist.Count; i++)
        {
            if (TutorialPanelist[i] != null)
            {
                TutorialPanelist[i].SetActive(i == Tutorial_Stage);
            }
        }
    }
    public void NextStage()
    {
        if (Tutorial_Stage < TutorialPanelist.Count - 1)
        {
            Tutorial_Stage++;
            isConditionMet = false;
            UpdateTutorialStage();
        }
        else
        {
            Debug.Log("Tutorial Completed!");
        }
    }
    private void CheckConditionsForCurrentStage()
    {
        switch (Tutorial_Stage)
        {
            case 0:
                gamestate.gamestate_Number = 4;
                isConditionMet = Input.GetKeyDown(KeyCode.W);
                break;

            case 1:
               
               isConditionMet = Input.GetKeyDown(KeyCode.A); 
                break;

            case 2:
               
                isConditionMet = Input.GetKeyDown(KeyCode.S); 
                break;
            case 3:
               
                isConditionMet = Input.GetKeyDown(KeyCode.D);
                break;
            case 4:
                ChairSingle chairsingle = GameObjTurtorial[0].GetComponent<ChairSingle>();
                isConditionMet = chairsingle.isSited;
                break;
            case 5:
                CheckCustomerPlacement();
                break;
            case 6:
                gamestate.gamestate_Number = 1;
                CheckTrashPlacement();
                break;
            default:
                isConditionMet = true; 
                break;
        }
        if (isConditionMet)
        {
            NextStage();
        }

    }
    public void CheckCustomerPlacement()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        if (customers.Length == 0)
        {
            Debug.Log("No customers found.");
            isConditionMet = true;
            return;
        }
    }

    public void CheckTrashPlacement()
    {
        GameObject[] Trashs = GameObject.FindGameObjectsWithTag("TrashTag");

        if (Trashs.Length == 0)
        {
            Debug.Log("No Trash found.");
            isConditionMet = true;
            return;
        }
    }
}
