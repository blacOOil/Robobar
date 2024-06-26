using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSingle : MonoBehaviour
{
    [Header("OrderSession")]
    public int Randomdrinkfloat,ServedDrink;
    public List<Sprite> OrderMenu;
    public Image OrderImage;
    public bool Isordered;
    public GameObject DrinkReceived;
    public LayerMask DrinkLayer;
    public Transform DrinkPlacement;
    public List<GameObject> DrinkPrefab;

    [Header("SittingSession")]
    public float PlayerCheckerRadius = 100f;
    public bool Issited,Isfull;
    public GameObject Requested,PlayerInteractMenu,exitdoor;
    public LayerMask Player;
    public CustomertoTable customertoTable;

    [Header("QueueSession")]
    public CustomerManager CustomerManager;

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
    private bool IsDrinkClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius, DrinkLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("drink"))
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
        exitdoor = GameObject.FindGameObjectWithTag("exitdoor");
        GameObject CustomerManagerObj = GameObject.Find("CustomerManager");
        CustomerManager = CustomerManagerObj.GetComponent<CustomerManager>();
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
                else if (Isordered == true)
                {
                    if (IsplayerClose() == true)
                    {
                        if (IsDrinkClose() == true)
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                ReceiveOrder();
                                if(Randomdrinkfloat == ServedDrink)
                                {
                                    SpawnDrinkThatRecived();
                                    StartCoroutine(drinking());
                                    Debug.Log("Thank");
                                   
                                }
                                else
                                {
                                    Debug.Log("Wrong");
                                }
                                
                            }
                        }

                    }
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
                        CustomerManager.RemoveSpawnedList();
                       
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
    public void ReceiveOrder()
    {
        float closestDistance = Mathf.Infinity;
        GameObject ClosestDrinks = null;

        GameObject[] drinkes = GameObject.FindGameObjectsWithTag("drink");
        foreach (GameObject drink in drinkes)
        {
            DrinkSingle drinkSingle = drink.GetComponent<DrinkSingle>();
            if (drinkSingle != null )
            {
                float distance = Vector3.Distance(transform.position, drink.transform.position);
                if (distance < closestDistance && distance <= PlayerCheckerRadius)
                {
                 
                    closestDistance = distance;
                    ClosestDrinks = drink;
                    ServedDrink = drink.GetComponent<DrinkSingle>().DrinkId;
                }

            }
        }
        DrinkReceived = ClosestDrinks;

    }
    public void SpawnDrinkThatRecived()
    {
        
        Instantiate(DrinkPrefab[ServedDrink], DrinkPlacement.position, DrinkPlacement.rotation);
    }
    IEnumerator drinking()
    {
        yield return new WaitForSeconds(10f);
        Isfull = true;
    }

}
