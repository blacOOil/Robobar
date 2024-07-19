using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSingle : MonoBehaviour
{
    public GameObject tableTag;
    public List<GameObject> Seat;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Seat[0].GetComponent<ChairSingle>().isSited == true)
        {
            tableTag.SetActive(false);
        }
        else
        {
            tableTag.SetActive(true);
        }
    }
}
