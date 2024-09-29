using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningState : MonoBehaviour
{
    public List<GameObject> alltabletag,TrashPosition,TrashGameObj;
    public bool Iscleaningstarted;
    public int gamestatenumber;
    public Gamestate gamestate;
    public Qsingle qsingle;
    public GameObject Trashbin;
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
            ClearCustomer();
            SpawningTrash();
        }
        else
        {
            ClearTrash();
        }
    }
    public void ClearTrash()
    {
        Trashbin.SetActive(false);
    }
    public void SpawningTrash()
    {
        Trashbin.SetActive(true);
    }
    public void UpdateGamestate()
    {
        gamestatenumber = gamestate.gamestate_Number;
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
    public void ClearCustomer()
    {
        bool Iscustomerblocked = true;
        qsingle.isCustomerhere = Iscustomerblocked;
        GameObject[] Allcustomer = GameObject.FindGameObjectsWithTag("Customer");
        foreach(GameObject customer in Allcustomer)
        {
            Destroy(customer);
        }
    }
}
