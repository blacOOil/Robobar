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
    public bool Isordered,IsorderRevied;
    public GameObject  DrinktoDrinked,ClosestPlayer;
    public LayerMask DrinkLayer;
    public Transform DrinkPlacement;
    public List<GameObject> DrinkPrefab;

    [Header("Satisfaction")]
    public float RemainmingTime, SatificationState;
    public bool IsGreen, IsYellow, IsRed, IsBlack;
    public Slider SatificationSlider;
    public Color newColor;
    public float changeTime = 2.98f;
    public Image SliderImage;
    public GameObject SatifactorCanvas;
    public bool IsDanceAdded = false;


    [Header("SittingSession")]
    public float PlayerCheckerRadius = 999f;
    public bool Issited, Isfull;
    public GameObject Requested, PlayerInteractMenu, exitdoor;
    public LayerMask Player;
    public CustomertoTable customertoTable;

    private void Start()
    {
        InitializeSession();
        IntiallizeSatificacian();
    }

    private void Update()
    {
        SatificationDecressing();
        HandleCustomerSatification();
        if (Isfull == false)
        {
            if (IsChairClose())
            {
                Issited = true;
            }
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
        IsorderRevied = false;
        PlayerInteractMenu.SetActive(false);
        Requested.SetActive(false);
        Issited = false;
        Isfull = false;
        Isordered = false;
        exitdoor = GameObject.FindGameObjectWithTag("exitdoor");
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
            ReceiveOrder();
            HandleOrderReceived();
        }
    }

    // Handle receiving the drink order and player interaction
    private void HandleOrderReceived()
    {
       
        if ((Isplayer1Close()|| Isplayer2Close()||Isplayer3Close()) && IsPlayerInput())
        {
           // ReceiveOrder();
            if (Randomdrinkfloat == ServedDrink && IsorderRevied == false )
            {
                SatifactorCanvas.SetActive(false);
                Destroy(OrderImage);
                SpawnDrinkThatRecived();
                StartCoroutine(DrinkingRoutine());
                SatificationIncrease();
                IsorderRevied = true;
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

        if (Isplayer1Close() || Isplayer2Close() || Isplayer3Close())
        {
            PlayerInteractMenu.SetActive(true);
            if (Isplayer1Close() )
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
            if (Isplayer3Close())
            {
                if (Input.GetKeyDown(KeyCode.Keypad9))
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
        if (Isordered == true)
        {
            customertoTable.GetUp();
        }
        if (DrinktoDrinked != null)
        {
            Destroy(DrinktoDrinked);
        }
        ExitStore();
        Destroy(gameObject);
    }

    private bool Isplayer1Close()
    {
        return IsPlayerClose("Player1");
    }

    private bool Isplayer2Close()
    {
        return IsPlayerClose("Player2");

    }
    private bool Isplayer3Close()
    {
        return IsPlayerClose("Player3");
    }

    private bool IsDrinkClose()
    {
        return CheckProximity("drink");
    }
    private bool IsChairClose()
    {
        return CheckProximity("Chairs");
    }
    private bool IsTagClose()
    {
        return CheckProximity("tableTag");
    }
    private bool IsPlayerInput()
    {
        if (Isplayer1Close())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                return true;
            }
        }
        else if (Isplayer2Close())
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                return true;
            }
        }
        else if (Isplayer3Close())
        {
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
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
    private bool IsPlayerClose(string playerTag)
    {
        if (ClosestPlayer == null)
        {
            if (CheckProximity(playerTag))
            {
                ClosestPlayer = GameObject.FindGameObjectWithTag(playerTag);
                return true; // Player is close and now assigned
            }
            return false; // No player close enough
        }
        else if (ClosestPlayer.CompareTag(playerTag) && !CheckProximity(playerTag))
        {
            // If the assigned player moves away, reset ClosestPlayer
            ClosestPlayer = null;
            return false; // Player is not close anymore
        }

        return ClosestPlayer.CompareTag(playerTag); // ClosestPlayer is already assigned and still close
    }

    public void OrderTheDrink()
    {
        SatificationIncrease();
        int RandomIndex = Random.Range(0, OrderMenu.Count);
        Debug.Log(RandomIndex);
        Randomdrinkfloat = RandomIndex;
        OrderImage.sprite = OrderMenu[RandomIndex];
        Isordered = true;
    }

    public void ReceiveOrder()
    {
        if(ClosestPlayer!= null) { 
            GameObject drink = ClosestPlayer.GetComponent<ServiceSystem>().Objholding;

            if (drink != null)
            {
                DrinkSingle drinkSingleComponent = drink.GetComponent<DrinkSingle>();


                if (drinkSingleComponent != null && Isordered == true)
                {
                    ServedDrink = drinkSingleComponent.DrinkId;
                   // Debug.Log("Drink is " + drinkSingleComponent.DrinkId);
                }
                else
                {
                 //   Debug.Log("Drink component not found on the held object.");
                    ServedDrink = -1; // Fallback value for error
                }
            }

    }

}

    public void SpawnDrinkThatRecived()
    {
        DrinktoDrinked = Instantiate(DrinkPrefab[Randomdrinkfloat], DrinkPlacement.position, DrinkPlacement.rotation);
        Rigidbody drinkRigidbody = DrinktoDrinked.GetComponent<Rigidbody>();
        if (drinkRigidbody != null)
        {
            // Freeze position on all axes
            drinkRigidbody.constraints = RigidbodyConstraints.FreezePositionX |
                                         RigidbodyConstraints.FreezePositionY |
                                         RigidbodyConstraints.FreezePositionZ;
            // Optionally freeze rotation as well
            drinkRigidbody.constraints |= RigidbodyConstraints.FreezeRotation;
        }

        // Set the collider to trigger
        Collider drinkCollider = DrinktoDrinked.GetComponent<Collider>();
        if (drinkCollider != null)
        {
            drinkCollider.isTrigger = true;
        }
        SatificationIncrease();
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
    public void IntiallizeSatificacian()
    {
        RemainmingTime = 60;
        IsGreen = true;
        IsYellow = false;
        IsRed = false;
        IsBlack = false;
    }
    public void SatificationDecressing()
    {
        RemainmingTime -= Time.deltaTime;
    }
    public void SatificationIncrease()
    {
        RemainmingTime += 10;
    }
    public void HandleCustomerSatification()
    {
        SatificationSlider.value = RemainmingTime;
        if (RemainmingTime > 40) // High satisfaction (Green)
        {
            IsGreen = true;
            IsYellow = false;
            IsRed = false;
            IsBlack = false;
        }
        else if (RemainmingTime > 20 && RemainmingTime <= 40) // Moderate satisfaction (Yellow)
        {
            IsGreen = false;
            IsYellow = true;
            IsRed = false;
            IsBlack = false;
        }
        else if (RemainmingTime > 0 && RemainmingTime <= 20) // Low satisfaction (Red)
        {
            IsGreen = false;
            IsYellow = false;
            IsRed = true;
            IsBlack = false;
        }
        else if (RemainmingTime <= 0) // Very low satisfaction (Black)
        {
            IsGreen = false;
            IsYellow = false;
            IsRed = false;
            IsBlack = true;
        }
        if (IsGreen)
        {
            newColor = Color.green;
            ColorChangeLoop();
        }
        else if (IsYellow)
        {
            newColor = Color.yellow;
            ColorChangeLoop();
        }
        else if (IsRed)
        {
            newColor = Color.red;
           ColorChangeLoop();
        }
        else if (IsBlack)
        {
            EndSession();
        }
    }
    public void ColorChangeLoop()
    {
        Color currentColor = SliderImage.color;
        SliderImage.color = Color.Lerp(currentColor, newColor, changeTime);
    }
}
    
