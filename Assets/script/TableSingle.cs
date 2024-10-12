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

        // Show the tableTag if not all seats are full, otherwise hide it
        if (IsAllnotfull)
        {
            tableTag.SetActive(true); // Show the tag if not all seats are full
        }
        else
        {
            tableTag.SetActive(false); // Hide the tag if all seats are full
        }
    }

    public void CheckSeat()
    {
        // Assume all seats are full initially
        IsAllnotfull = false;

        foreach (GameObject chair in Seat)
        {
            if (!chair.GetComponent<ChairSingle>().isSited)
            {
                IsAllnotfull = true; // Set to true if any seat is not occupied
                break;
            }
        }
    }
}
