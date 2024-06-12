using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSingle : MonoBehaviour
{
    [Header("OrderSession")]
    public float Randomdrinkfloat;
    public List<Sprite> OrderMenu;
    public Image OrderImage;
    public bool Isordered;

    [Header("OrderSession")]
    public float PlayerCheckerRadius = 100f;
    public bool Issited,Isfull;
    public GameObject Requested,PlayerInteractMenu,exitdoor;
    public LayerMask Player;
    public CustomertoTable customertoTable;
    public GameObject[] drinkorder;

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
        Isordered = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Isfull == false)
        {
            if (Issited)
            {
                PlayerInteractMenu.SetActive(false);
                Requested.SetActive(true);
                if(Isordered == false)
                {
                Orderthedrink();
                }
                else
                {

                }

              
               
                
               

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
                        Issited = true;
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
   
    public void exitStore()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, exitdoor.transform.position, 4f);
    }
    public void Orderthedrink()
    {
        
        int RandomIndex = Random.Range(0,OrderMenu.Count);
        Debug.Log(RandomIndex);
        Randomdrinkfloat = RandomIndex;
        OrderImage.sprite = OrderMenu[RandomIndex];
        Isordered = true;
    }

}
