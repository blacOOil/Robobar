using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomertoTable : MonoBehaviour
{
    public GameObject Self;
    public GameObject Table;
    public float Speed;
    public float searchRadius = 900000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void movetotable()
    {
        findClosestTable();
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
                    chair.GetComponent<ChairSingle>().isSited = true;
                    closestDistance = distance;
                    ClosestChairs = chair;
                }

            }
        }
        Table = ClosestChairs;

       
    }
}
