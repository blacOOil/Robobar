using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingState : MonoBehaviour
{
    public List<GameObject> UpgradingSessionGameObj;
    public GameObject OpeningShopBox;
    public Gamestate gamestate;
    public bool IsupgradingStarted;
    public int gamestatenumber;
    // Start is called before the first frame update
    void Start()
    {
        IsupgradingStarted = false;
        AddUpgradingGameObj();
        OpeningShopBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamestate();
        foreach (GameObject obj in UpgradingSessionGameObj)
        {
            if (IsupgradingStarted == true)
            {
                obj.SetActive(true);
                OpeningShopBox.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
                OpeningShopBox.SetActive(false);
            }
        }
    }
    public void UpdateGamestate()
    {
        gamestatenumber = gamestate.gamestate_Number;
        if (gamestatenumber == 3)
        {
            IsupgradingStarted = true;
        }
        else
        {
            IsupgradingStarted = false;
        }
    }
    public void AddUpgradingGameObj()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("UpgradTag");
        UpgradingSessionGameObj = new List<GameObject>(objectsWithTag);
    }
}
