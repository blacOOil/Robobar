using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSingle : MonoBehaviour
{
    public GameObject tableTag;
    public List<GameObject> Seat;
    public int ChairPlacement;
    public bool IsAllnotfull;

    // Start is called before the first frame update
    void Start()
    {
        IsAllnotfull = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSeat();

        if (IsAllnotfull)
        {
            tableTag.SetActive(true); 
        }
        else
        {
            tableTag.SetActive(false); 
        }
    }

    public void CheckSeat()
    {
        // Reset IsAllnotfull to true before checking all seats
        IsAllnotfull = true;

        foreach (GameObject chair in Seat)
        {
            if (chair.GetComponent<ChairSingle>().isSited)
            {
                IsAllnotfull = false; // If any seat is occupied, set to false
                break;
            }
        }
    }
}
