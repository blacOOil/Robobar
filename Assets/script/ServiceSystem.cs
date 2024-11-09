using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSystem : MonoBehaviour
{
    public MonneyLevelCode monneyLevelCode;
    public Gamestate gamestate;
    public Transform Hand;
    public bool holdedrink, IsreadytoServered, holdetable, IsTableTagnear, Ishandholded;
    private float CustomerCheckerRadius = 2f,SpawnerRadius = 100f;
    private int playernumber = 0;
    public LayerMask CustomerLayer,TagLayer,SpawnerLayer;
    public GameObject ClosestCustomer, Objholding,ReadytoPickObj,ClosestTage,Spawner;
    public BoxSkill boxSkill;
    public bool IshadBoxskill = false, IsNextDrinkSpawned = false, IscustmerDrinkReceived = false;
    public List<GameObject> ExtraHand,ExtraDrink;

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

        if(gameObject.tag == "Player2")
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
        GameObject[] ExtrahandArray = boxSkill.CollectedItem;
        ExtraHand = new List<GameObject>();
        
        foreach(GameObject obj in ExtrahandArray)
        {
            if (obj != null)
            {
                ExtraHand.Add(obj);
            }
        }

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
        monneyLevelCode.moneyAdd();
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

                    // Destroy the drink and reset states
                    drink.selfDestruct();
                    holdedrink = false;
                    Ishandholded = false;
                    Objholding = null;
                    IsNextDrinkSpawned = false;

                    // Check if IshadBoxskill is true and ExtraDrink has at least one element
                    if (IshadBoxskill && ExtraDrink != null && ExtraDrink.Count > 0 && ExtraDrink[0] != null)
                    {
                        Objholding = ExtraDrink[0];
                        NextDrinkSpawned();
                        holdedrink = true;
                        Ishandholded = true;
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
        GameObject firstDrink = ExtraDrink[0];
        firstDrink.transform.SetParent(Hand); // Detach from parent if needed
        firstDrink.transform.position = new Vector3(0, 1, 0); // Set the desired spawn position
        firstDrink.transform.localScale = Vector3.one; // Reset size if necessary
        firstDrink.GetComponent<MeshRenderer>().enabled = true; // Make it visible
        firstDrink.GetComponent<Rigidbody>().isKinematic = false; // Restore physics if needed
        firstDrink.GetComponent<Collider>().isTrigger = false; // Restore collider

        ExtraDrink.RemoveAt(0);
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
        if (ExtraDrink.Count >= 2)
        {
            Debug.Log("Cannot store more than 3 drinks.");
            return;
        }

        // Add the drink to the ExtraHand list
        ExtraDrink.Add(Drink);

        TranformDrinktoCollecter(Drink);
    }
    public void TranformDrinktoCollecter(GameObject Drink)
    {
         int PlaceMent = ExtraDrink.Count ;
        Drink.GetComponent<MeshRenderer>().enabled = false;
      
        Drink.transform.SetParent(ExtraHand[PlaceMent].transform);
        Drink.transform.localPosition = Vector3.zero;
        Drink.transform.localRotation = Quaternion.identity;
        Drink.GetComponent<Rigidbody>().isKinematic = true;
        Drink.GetComponent<Collider>().isTrigger = true;

        Drink.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drink"))
        {
            if (Ishandholded == false)
            {
                TransformObjToHand(collision.gameObject);
            }
            else
            {
                if (IshadBoxskill)
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
        Objholding.GetComponent<Rigidbody>().isKinematic = false;
        if (Objholding.CompareTag("drink"))
        {
            Objholding.transform.SetParent(null);
            Objholding.GetComponent<Collider>().isTrigger = false;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                return true;
            }
        }
        if(playernum == 1)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                return true;
            }
        }
        if (playernum == 2)
        {
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                return true;
            }
        }
        return false;
    }
}
   