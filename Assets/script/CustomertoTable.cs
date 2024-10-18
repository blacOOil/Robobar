using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomertoTable : MonoBehaviour
{
    [Header("CustomerSetting")]
    public SeatSetSingle seatSetSingle;
    public bool IstableTagnear;
    public GameObject Self;
    public GameObject Table,TableTag,nearTabletag;
    public float Speed;
    public float searchRadius = 900000f;

    [Header("CustomerSpawning")]
    private int Chairnum;
    public List<GameObject> ExtraChair,ExtraCustomer;
    private bool IsselfSitted;
    

    // Start is called before the first frame update
    void Start()
    {
        IsselfSitted = false;
        IstableTagnear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsselfSitted == true)
        {
            SpawnedExtraCustomer();
        }
    }
    public void movetotable()
    {
        if (fixedTableset())
        {
            Self.transform.position = Table.transform.position;
            IsselfSitted = true;

        }
        else
        {
            Debug.Log("No table nearby.");
        }
       
        
    }
    public bool fixedTableset()
    {
        //findClosestTable();
        GettableId();
        if (Table == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void findClosestTable()
    {
        float closestDistance = Mathf.Infinity;
        GameObject ClosestChairs = null;

        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Chairs");
        foreach (GameObject chair in chairs)
        {
            ChairSingle chairSingle = chair.GetComponent<ChairSingle>();
            if (chairSingle != null && !chairSingle.isSited)
            {
                float distance = Vector3.Distance(transform.position, chair.transform.position);
                if (distance < closestDistance && distance <= searchRadius)
                { 
                    closestDistance = distance;
                    ClosestChairs = chair;
                    chair.GetComponent<ChairSingle>().isSited = true;
                }

            }
        }
        Table = ClosestChairs;

       
    }
    public void GettableId()
    {
        if (IstableTagnear == true)
        {
            seatSetSingle = nearTabletag.GetComponent<SeatSetSingle>();
            List<GameObject> seatList = seatSetSingle.seat;
            Chairnum = seatList.Count;

            // Loop through all seats to find the first available (not occupied) seat
           
            foreach (GameObject seat in seatList)
            {
                ChairSingle chairSingle = seat.GetComponent<ChairSingle>();

                // If the seat is not occupied, set it as the table and mark it as occupied
                if (!chairSingle.isSited)
                {
                    Table = seat;
                 
                    break; // Exit the loop once a seat is found
                }
            }

            if (Table == null)
            {
                Debug.Log("All seats are occupied.");
            }
        }
        else
        {
            Debug.Log("No tabletag");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tableTag"))
        {
            IstableTagnear = true;
            nearTabletag = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tableTag"))
        {
            IstableTagnear = false;
           // nearTabletag = null;
        }
    }
    public void GetUp()
    {
        ChairSingle chairSingle = Table.GetComponent<ChairSingle>();
        chairSingle.isSited = false;
    }
    public void SpawnedExtraCustomer()
    {
        List<GameObject> SeatList = seatSetSingle.seat;
        foreach (GameObject Seat in SeatList)
        {
            ChairSingle chairSingle = Seat.GetComponent<ChairSingle>();
            if (!chairSingle.isSited)
            {

            }
        }
    }
}
