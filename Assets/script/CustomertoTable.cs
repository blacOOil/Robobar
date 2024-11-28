using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomertoTable : MonoBehaviour
{
    [Header("CustomerSetting")]
    public SeatSetSingle seatSetSingle;
    public bool IstableTagnear;
    public GameObject Self;
    public GameObject Table, TableTag, nearTabletag;
    public float Speed;
    public float searchRadius = 900000f;
    Animator anim;
    public HumannoidCustomer humannoidCustomer;

    [Header("CustomerSpawning")]
    private int Chairnum;
    public List<GameObject> ExtraChair, ExtraCustomer;
    private bool IsselfSitted;


    // Start is called before the first frame update
    void Start()
    {
        humannoidCustomer = gameObject.GetComponent<HumannoidCustomer>();
        IsselfSitted = false;
        IstableTagnear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsselfSitted == true)
        {
            SpawnedExtraCustomer();
           
        }
       if(gameObject.GetComponent<CustomerSingle>().Issited == true)
        {
            anim = GetComponentInChildren<Animator>();
            anim.GetComponent<Animator>().SetBool("IsSit", true);
        }
    }
    public void movetotable()
    {
        if (fixedTableset())
        {
            Self.transform.position = Table.transform.position;

            if (humannoidCustomer != null) {
                Debug.Log("In Site");
                Vector3 newposition = Self.transform.position;
                newposition.y = -0.32f;
                Self.transform.position = newposition;
            }

            IsselfSitted = true;

           // anim = GetComponentInChildren<Animator>();
           // anim.GetComponent<Animator>().SetBool("IsSit", true);
        }
        else
        {

            Debug.Log("No table nearby.");
            Destroy(gameObject);
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

    }
    public void SpawnedExtraCustomer()
    {
        List<GameObject> SeatList = seatSetSingle.seat;

        // Create a list of free seats starting from seatList[1]
        List<GameObject> freeSeats = new List<GameObject>();

        // Start from seatList[1] to avoid seatList[0]
        for (int i = 1; i < SeatList.Count; i++)
        {
            ChairSingle chairSingle = SeatList[i].GetComponent<ChairSingle>();
            if (!chairSingle.isSited)
            {
                freeSeats.Add(SeatList[i]);  // Add the seat to the freeSeats list if it's not occupied
            }
        }

        // Determine the random number of customers to spawn based on the available free seats
        int randomCustomerCount = Random.Range(0, freeSeats.Count); // Adjust range as needed

        int customersSpawned = 0; // Keep track of how many customers have been spawned

        foreach (GameObject Seat in freeSeats)
        {
            ChairSingle chairSingle = Seat.GetComponent<ChairSingle>();

            // Check if the seat is free and if we still need to spawn more customers
            if (!chairSingle.isSited && customersSpawned < randomCustomerCount)
            {
                // Instantiate a new customer at the seat's position
                GameObject newCustomer = Instantiate(ExtraCustomer[Random.Range(0, ExtraCustomer.Count)], Seat.transform.position, Quaternion.identity);

                // Set the customer as seated
                newCustomer.GetComponent<CustomerSingle>().Issited = true;
                chairSingle.isSited = true;  // Mark the seat as occupied

                // Increment the count of spawned customers
                customersSpawned++;
            }

            // If we have spawned the required number of customers, break the loop
            if (customersSpawned >= randomCustomerCount)
            {
                break;
            }
        }
    }

}
