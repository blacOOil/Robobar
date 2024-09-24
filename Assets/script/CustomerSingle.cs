using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSingle : MonoBehaviour
{
    [Header("OrderSession")]
    public int Randomdrinkfloat, ServedDrink;
    public List<Sprite> OrderMenu;
    public Image OrderImage;
    public bool Isordered;
    public GameObject DrinkReceived, DrinktoDrinked;
    public LayerMask DrinkLayer;
    public Transform DrinkPlacement;
    public List<GameObject> DrinkPrefab;

    [Header("Satisfaction")]
    public float RemainmingTime;

    [Header("SittingSession")]
    public float PlayerCheckerRadius = 100f;
    public bool Issited, Isfull;
    public GameObject Requested, PlayerInteractMenu, exitdoor;
    public LayerMask Player;
    public CustomertoTable customertoTable;

    private void Start()
    {
        InitializeSession();
    }

    private void Update()
    {
        if (Isfull == false)
        {
            ManageSittingAndOrders();
        }
        else
        {
            EndSession();
        }
    }

    // Separate into methods

    // Initialization method for starting the session
    private void InitializeSession()
    {
        PlayerInteractMenu.SetActive(false);
        Requested.SetActive(false);
        Issited = false;
        Isfull = false;
        Isordered = false;
        exitdoor = GameObject.FindGameObjectWithTag("exitdoor");
        RemainmingTime = 200;
    }

    // Handle sitting session and managing orders
    private void ManageSittingAndOrders()
    {
        if (Issited)
        {
            ManageOrderProcess();
        }
        else
        {
            HandlePlayerInteraction();
        }
    }

    // Manage the drink order process when the customer is seated
    private void ManageOrderProcess()
    {
        PlayerInteractMenu.SetActive(false);
        Requested.SetActive(true);

        if (Isordered == false)
        {
            OrderTheDrink();
        }
        else
        {
            HandleOrderReceived();
        }
    }

    // Handle receiving the drink order and player interaction
    private void HandleOrderReceived()
    {
        RemainmingTime -= Time.deltaTime;

        if (IsplayerClose() && IsDrinkClose() && Input.GetKeyDown(KeyCode.E))
        {
            ReceiveOrder();
            if (Randomdrinkfloat == ServedDrink)
            {
                SpawnDrinkThatRecived();
                StartCoroutine(DrinkingRoutine());
                Debug.Log("Thank");
            }
            else
            {
                Debug.Log("Wrong");
            }
        }
    }

    // Handle player interaction when the customer is not yet seated
    private void HandlePlayerInteraction()
    {
        
        if (IsplayerClose() || Isplayer2Close())
        {
            PlayerInteractMenu.SetActive(true);
            if (IsplayerClose())
            {
                
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Go to table Ok");
                        customertoTable.movetotable();
                    if (!customertoTable.fixedTableset())
                    {
                        ExitStore();
                    }
                    else
                    {
                        Issited = true;
                    }
                        Issited = true;
                    }
                

            }
            if (Isplayer2Close())
            {
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        Debug.Log("Go to table Ok");
                        customertoTable.movetotable();
                    if (!customertoTable.fixedTableset())
                    {
                        ExitStore();
                    }
                    else
                    {
                        Issited = true;
                    }
                    Issited = true;

                    }
                
            }


        }
        else
        {
            PlayerInteractMenu.SetActive(false);
        }

        Requested.SetActive(false);
    }

    // End the session when the customer is full
    private void EndSession()
    {
        customertoTable.GetUp();
        Destroy(DrinktoDrinked);
        ExitStore();
        Destroy(gameObject);
    }

    private bool IsplayerClose()
    {
        return CheckProximity("Player1");
    }

    private bool Isplayer2Close()
    {
        return CheckProximity("Player2");
    }

    private bool IsDrinkClose()
    {
        return CheckProximity("drink");
    }

   

    private bool CheckProximity(string tag)
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }


    public void OrderTheDrink()
    {
        int RandomIndex = Random.Range(0, OrderMenu.Count);
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
            if (drinkSingle != null)
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
        DrinktoDrinked = Instantiate(DrinkPrefab[ServedDrink], DrinkPlacement.position, DrinkPlacement.rotation);
    }

    public void ExitStore()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, exitdoor.transform.position, 4f);
    }

    IEnumerator DrinkingRoutine()
    {
        yield return new WaitForSeconds(10f);
        Isfull = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerCheckerRadius);
    }
}
