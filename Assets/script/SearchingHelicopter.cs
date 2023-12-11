using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingHelicopter : MonoBehaviour
{
    public Transform[] patrolpoint;
    public int target;
    public float Speed;
    public Transform Player;
    private bool IsplaerDetected;
   
    // Start is called before the first frame update
    void Start()
    {
        IsplaerDetected = false;
        target = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsplaerDetected == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else { 
        
        if (transform.position == patrolpoint[target].position)
        {
            increaseTargetint();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolpoint[target].position,Speed*Time.deltaTime);
        }
    }
    void increaseTargetint()
    {
         target++;
        if(target >= patrolpoint.Length)
        {
            target = 0;
        }
        
    }
     void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IsplaerDetected = true;
        }
    }
    

}
