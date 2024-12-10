using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Gamestate gamestate;
    public Transform Hand;
    public bool holdedrink, IsreadytoServered, holdetable, IsTableTagnear, Ishandholded,IsreadytopickNext;
    private float CustomerCheckerRadius = 2f,SpawnerRadius = 100f;
    private int playernumber = 0;
    public int ExtraSpaceLimit;
    public LayerMask CustomerLayer,TagLayer,SpawnerLayer;
    public GameObject ClosestCustomer, Objholding,ReadytoPickObj,ClosestTage,Spawner;
    public BoxSkill boxSkill;
    public bool IshadBoxskill = false, IsNextDrinkSpawned = false, IscustmerDrinkReceived = false;
    public List<GameObject> ExtraDrinkId;
    public PlayerScoreSystem playerScoreSystem;

 
   public bool IsSpawnerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SpawnerRadius, SpawnerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("ServiceSpawner"))
            {
                return true;

            }
        }
        return false;
    }
    private bool IscustomerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CustomerCheckerRadius, CustomerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Customer"))
            {
                return true;
                
            }
        }
        return false;
    }
    private bool IsTageClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CustomerCheckerRadius, TagLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("tableTag"))
            {
                if (!Ishandholded)
                {
                    ReadytoPickObj = hitCollider.gameObject; // Store the table tag object
                }
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
    
        BoxSkillCheck();
        Spawner = GameObject.FindGameObjectWithTag("ServiceSpawner");
        gamestate = GameObject.Find("LevelManager").GetComponent<Gamestate>();
        Objholding = null;
        holdedrink = false;
        IsreadytoServered = false;
        holdetable = false;
        IsTableTagnear = false;
        Ishandholded = false;
        monneyLevelCode = FindObjectOfType<MonneyLevelCode>();
        playerScoreSystem = gameObject.GetComponent<PlayerScoreSystem>();

        if (gameObject.tag == "Player2")
        {
            playernumber = 1;
        }
        if (gameObject.tag == "Player3")
        {
            playernumber = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gamestate.gamestate_Number != 4)
        {
            ReleaseDrink();
        }
        if (IshadBoxskill)
        {
            InputMoreHand();
            
        }
        
        if (IsSpawnerClose() && gamestate.gamestate_Number == 4 )
        {
          
        }
        else
        {
            gameObject.transform.position = Spawner.transform.position;
        }
        if (IsTageClose()&& Ishandholded == false)
        {  
                IsTableTagnear = true;
        }
        else
        {
            IsTableTagnear = false;
        }
        if ((holdedrink) && !IscustomerClose())
        {
            if (PlayerInput(playernumber))
            {
                ReleaseDrink();
            }
        }
        if(holdetable)
        {
            if (PlayerInput(playernumber))
            {
                ReleaseDrink();
            }
        }

        if (holdedrink && IscustomerClose())
        {
            IsreadytoServered = true;
        }
        else
        {
            IsreadytoServered = false;
        }

        if (IsreadytoServered)
        {
            findClosestCustomer(); // Ensure this is called to find the closest customer

            if (PlayerInput(playernumber))
            {
                ServiceProceed();
            }
        }
        if (holdedrink == false && IsTableTagnear)
        {
            if (PlayerInput(playernumber))
            {
            TransformObjToHand(ReadytoPickObj);
            }

        }
       
       
        
        if(Ishandholded == false)
        {
            holdedrink = false;
            holdetable = false;
        }
    }
    public void InputMoreHand()
    {
    
    }
    public void BoxSkillCheck()
    {
        // Attempt to get the BoxSkill component
        boxSkill = gameObject.GetComponent<BoxSkill>();

        // Check if BoxSkill is null (i.e., not found)
        if (boxSkill == null)
        {
            IshadBoxskill = false; // Indicate that the skill is not present
         //   Debug.LogWarning("BoxSkill component not found on this game object.");
        }
        else
        {
            IshadBoxskill = true; // Indicate that the skill is present
        }
    }
    public void ServiceProceed()
    {
        
        findClosestCustomer();

        if (holdedrink)
        {
            if (ClosestCustomer != null)
            {
                CustomerSingle customer = ClosestCustomer.GetComponent<CustomerSingle>();
                DrinkSingle drink = Objholding?.GetComponent<DrinkSingle>();

                if (customer != null && drink != null &&
                    customer.Randomdrinkfloat == drink.DrinkId &&
                    !customer.IsorderRevied)
                {
                    Debug.Log("ServiceProceed");
                    monneyLevelCode.moneyAdd();
                    // Destroy the drink and reset states
                    drink.selfDestruct();
                    holdedrink = false;
                    Ishandholded = false;
                    Objholding = null;
                    IsNextDrinkSpawned = false;
                   
                    if(playerScoreSystem != null)
                    {
                        playerScoreSystem.ScoreAdd();
                    }
                    if (IshadBoxskill)
                    {
                        SpawnNextDrink();
                    }


                }
                else
                {
                    ReleaseDrink();
                }
            }
            else
            {
                Debug.LogWarning("Closest customer not found.");
                ReleaseDrink();
            }
        }
        else
        {
            Debug.LogWarning("No drink is currently being held.");
        }
    }

    public void NextDrinkSpawned()
    {
        if(IsNextDrinkSpawned == false)
        {
        ExtraDrinkId.RemoveAt(0);
        }
    }
    public void findClosestCustomer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestCus = null;

        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            CustomerSingle customerSingle = customer.GetComponent<CustomerSingle>();
            if (customerSingle != null && customerSingle.Isordered)
            {
                float distance = Vector3.Distance(transform.position, customer.transform.position);
                if (distance < closestDistance && distance <= CustomerCheckerRadius)
                {
                    closestDistance = distance;
                    closestCus = customer;
                }
            }
        }

        ClosestCustomer = closestCus;

        if (ClosestCustomer != null)
        {
            Debug.Log("Closest customer found: " + ClosestCustomer.name);
        }
        else
        {
            Debug.Log("No close customer found.");
        }
    }
    public void StorExtraDrink(GameObject Drink)
    {

        // Check if the list already has 3 items
        if (ExtraDrinkId.Count >= ExtraSpaceLimit)
        {

        }
        else
        {

            TranformDrinktoCollecter(Drink);
        }

       
    }
    public void TranformDrinktoCollecter(GameObject Drink)
    {
        int DrindNum = Drink.GetComponent<DrinkSingle>().DrinkId;
        boxSkill.CollectedId.Add(DrindNum);
        Destroy(Drink);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drink"))
        {
            if (Ishandholded == false)
            {
                TransformObjToHand(collision.gameObject);
                collision.gameObject.GetComponent<DrinkSingle>().IsHeld = true;
            }
            else
            {
                if (IshadBoxskill && !collision.gameObject.GetComponent<DrinkSingle>().IsHeld)
                {
                   
                    StorExtraDrink(collision.gameObject);
                }
            }

        }
       
    }
   
 

        private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tableTag"))
        {
              IsTableTagnear = false;
            
        }
    }

    private void TransformObjToHand(GameObject Obj)
    {
        // Set the drink's position and parent to the hand
        Obj.transform.SetParent(Hand);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        Objholding = Obj;
        if (Obj.CompareTag("tableTag"))
        {
            holdetable = true;
         
        }
        if (Obj.CompareTag("drink"))
        {
            holdedrink = true;
            Obj.GetComponent<DrinkSingle>().IsHeld = true;
        }
        if (Obj == null)
        {

        }
        {

        }

        // Freeze the drink's position by setting its Rigidbody to kinematic
        Rigidbody rb = Obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Set the drink's collider to trigger
        Collider collider = Obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        Ishandholded = true;
    }
    public void ReleaseDrink()
    {
        if (Objholding != null) {
            Objholding.GetComponent<Rigidbody>().isKinematic = false;
            if (Objholding.CompareTag("drink"))
            {
                Objholding.transform.SetParent(null);
                Objholding.GetComponent<Collider>().isTrigger = false;
                holdedrink = false;
                //  Objholding.GetComponent<DrinkSingle>().IsHeld = false;
                IsNextDrinkSpawned = false;
                if (IshadBoxskill)
                {
                    SpawnNextDrink();
                }
            }
            else if(Objholding.CompareTag("tableTag"))
            {
                GameObject tabletagPlace = Objholding.GetComponent<SeatSetSingle>().TableTagTranform;
                Objholding.transform.position = tabletagPlace.transform.position;
                Objholding.transform.SetParent(tabletagPlace.transform);
                Objholding.GetComponent<Collider>().isTrigger = true;
            }

            StartCoroutine(DropDelay()); 
        }
    }
    public void SpawnNextDrink() 
    {
        
       if(boxSkill.CollectedId.Count >= 1)
        {
            Debug.Log("Spawned");
            // StartCoroutine(DropDelay());
            int NextDrinkId = boxSkill.CollectedId[0];
            GameObject NextDrink = boxSkill.CollectedItemSpace[NextDrinkId];
            Instantiate(NextDrink, Hand);
            NextDrink.GetComponent<Collider>().isTrigger = false;
            boxSkill.CollectedId.RemoveAt(0);
        }
        else
        {

        }
          

        
        
    }
    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(1f);
        Ishandholded = false;
        Objholding = null;
    }
    public bool PlayerInput(int playernum)
    {
        if(playernum == 0)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Player1Action"))
            {
                return true;
            }
        }
        if(playernum == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Player2Action"))
            {
                return true;
            }
        }
        if (playernum == 2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetButtonDown("Player3Action"))
            {
                return true;
            }
        }
        return false;
    }
}
   