using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingState : MonoBehaviour
{
    public GameObject UpgradingSessionGameObj;
    public Gamestate gamestate;
    public bool IsupgradingStarted;
    public int gamestatenumber;
    // Start is called before the first frame update
    void Start()
    {
        IsupgradingStarted = false;
        UpgradingSessionGameObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamestate();
        if(IsupgradingStarted == true)
        {
            UpgradingSessionGameObj.SetActive(true);
        }
        else
        {
            UpgradingSessionGameObj.SetActive(false);
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
}
