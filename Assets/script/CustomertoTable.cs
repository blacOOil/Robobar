using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomertoTable : MonoBehaviour
{
    public SeatSetSingle seatSetSingle;
    public bool IstableTagnear;
    public GameObject Self;
    public GameObject Table,TableTag,nearTabletag;
    public float Speed;
    public float searchRadius = 900000f;
    // Start is called before the first frame update
    void Start()
    {
        IstableTagnear = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void movetotable()
    {
        GettableId();
       // findClosestTable();
        Self.transform.position = Vector3.MoveTowards(Self.transform.position, Table.transform.position, Speed);
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
        if(IstableTagnear == true)
        {
            seatSetSingle = nearTabletag.GetComponent<SeatSetSingle>();
            List<GameObject> seatList = seatSetSingle.seat;
            Table = seatList[0];
            ChairSingle chairSingle = Table.GetComponent<ChairSingle>();
            chairSingle.isSited = true;
           

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
}
