using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSingle : MonoBehaviour
{
    public float PlayerCheckerRadius = 100f;
    public bool Issited,Isfull;
    public GameObject Requested,PlayerInteractMenu,exitdoor;
    public LayerMask Player;
    public CustomertoTable customertoTable;

    private bool IsplayerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius, Player);
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position to visualize the PlayerCheckerRadius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerCheckerRadius);
    }



    // Start is called before the first frame update
    void Start()
    {
     
        PlayerInteractMenu.SetActive(false);
        Requested.SetActive(false);
        Issited = false;
        Isfull = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Isfull == false)
        {
            if (Issited)
            {

               Requested.SetActive(true);
            
            }
            else
            {
                  if(IsplayerClose() == true)
            {
                     PlayerInteractMenu.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Go to table Ok");
                        customertoTable.movetotable();
                    }
            }
            else
            {
                PlayerInteractMenu.SetActive(false);
            }
            Requested.SetActive(false);
            }
           
         }
        else
        {
            exitStore();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chairs"))
        {
            Issited = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Chairs")) 
        {
            Issited = false;
        }
    }
    public void exitStore()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, exitdoor.transform.position, 4f);
    }

}
