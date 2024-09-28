using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningState : MonoBehaviour
{
    public List<GameObject> alltabletag;
    public bool Iscleaningstarted;
    private int gamestatenumber;
    public Gamestate gamestate;
    // Start is called before the first frame update
    void Start()
    {
        Iscleaningstarted = false;
        addtabletaggameobj();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamestate();
        if(gamestatenumber == 1)
        {
            Iscleaningstarted = true;
        }else
        { Iscleaningstarted = false; }
        if(Iscleaningstarted == true)
        {
            clearalltabletag();
        }
    }
    public void UpdateGamestate()
    {
        gamestate.gamestate_Number = gamestatenumber;
    }
    public void clearalltabletag()
    {
        foreach(GameObject tag in alltabletag)
        {
            tag.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void addtabletaggameobj()
    {
        GameObject[] tabletag = GameObject.FindGameObjectsWithTag("tableTag");
        foreach (GameObject tag in tabletag) 
        {
            alltabletag.Add(tag);
        }
    }
}
