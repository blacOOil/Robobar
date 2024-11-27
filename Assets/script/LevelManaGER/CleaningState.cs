using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningState : MonoBehaviour
{
    public List<GameObject> alltabletag, TrashPosition, TrashGameObj;
    public bool Iscleaningstarted, IsTrashCounted;
    public int gamestatenumber;
    private int trashAmount, TrashCount;
    public Gamestate gamestate;
    public Qsingle qsingle;
    public GameObject Trashbin;
    public float maxOffset = 2.0f;
    public bool Iscustomerblocked;
    public cookingSystem cookingSystem;
    private List<GameObject> spawnedTrash = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Iscleaningstarted = false;
        addtabletaggameobj();
        trashAmount = 0;
        IsTrashCounted = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGamestate();
        if (gamestatenumber == 1)
        {
            Iscustomerblocked = true;
            Iscleaningstarted = true;
            cookingSystem.enabled = false;

        } else
        {
            Iscleaningstarted = false;
            trashAmount = 0;
           // cookingSystem.enabled = true;

        }
        if (gamestatenumber == 4)
        {
            cookingSystem.enabled = true;
        }
        if (Iscleaningstarted == true)
        {
            clearalltabletag();
            ClearThing();
            if (trashAmount <= 5)
            {
                SpawningTrash();
            }
            TrashCounting();


        }
        else
        {
            ClearTrash();
            
        }
        if(gamestatenumber == 4)
        {
            foreach (GameObject tag in alltabletag)
            {
                tag.GetComponent<MeshRenderer>().enabled = true;
            }
            UnblockCustomer();
         
        }

        
    }
    public void UnblockCustomer()
    {
        if(Iscustomerblocked == true)
        {
            qsingle.isCustomerhere = false;
            Iscustomerblocked = false;
        }
        else
        {

        }
    }
    public void ClearTrash()
    {
        Trashbin.SetActive(false);
        DestroyAllTrash(); // Destroy all spawned trash
       
    }
    public void SpawningTrash()
    {
        Trashbin.SetActive(true);

        // Get a random index for TrashPosition and TrashGameObj
        int randomPositionIndex = Random.Range(0, TrashPosition.Count);
        int randomObjectIndex = Random.Range(0, TrashGameObj.Count);

        // Instantiate the random TrashGameObj at the random TrashPosition
        Vector3 basePosition = TrashPosition[randomPositionIndex].transform.position;

        // Create a random offset within the maxOffset range
        Vector3 randomOffset = new Vector3(
            Random.Range(-maxOffset, maxOffset), // X offset
            0,                                  // Y offset (assuming objects stay on the ground)
            Random.Range(-maxOffset, maxOffset)  // Z offset
        );

        // Add the random offset to the base position
        Vector3 spawnPosition = basePosition + randomOffset;

        // Instantiate the random TrashGameObj at the new random position
        GameObject spawned = Instantiate(TrashGameObj[randomObjectIndex], spawnPosition, Quaternion.identity);

        // Add the spawned trash to the list
        spawnedTrash.Add(spawned);

        trashAmount++;
    }
    public void DestroyAllTrash()
    {
        foreach (GameObject trash in spawnedTrash)
        {
            if (trash != null) // Check if the object still exists
            {
                Destroy(trash);
            }
        }

        // Clear the list after destroying all trash
        spawnedTrash.Clear();
        trashAmount = 0;
    }
    public void UpdateGamestate()
    {
        gamestatenumber = gamestate.gamestate_Number;
    }
    public void clearalltabletag()
    {
        foreach (GameObject tag in alltabletag)
        {
            tag.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void Setbackalltabletag()
    {
        foreach (GameObject tag in alltabletag)
        {
            tag.GetComponent<MeshRenderer>().enabled = true;
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
    public void ClearThing()
    {
        
        qsingle.isCustomerhere = Iscustomerblocked;
        GameObject[] Allcustomer = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in Allcustomer)
        {
            Destroy(customer);
        }
        GameObject[] Alldrink = GameObject.FindGameObjectsWithTag("drink");
        foreach (GameObject drink in Alldrink)
        {
            Destroy(drink);
        }

    }
    public void TrashCounting()
    {
        GameObject[] allTrashObjects = GameObject.FindGameObjectsWithTag("TrashTag");
        TrashCount = allTrashObjects.Length;
        handleTrashClearing();

    }
    public void handleTrashClearing()
    {
        if (TrashCount == 0)
        {
            EndCleaningSession();
        }
    }
    public void EndCleaningSession()
    {
        Setbackalltabletag();
        Iscleaningstarted = true;
        gamestate.gamestate_Number = 2;
        }
}
