using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingSystem : MonoBehaviour
{
    public GameObject cookingCanvas, MenuCanvas, ClosestPlayer, Holdindicator,CurrentMinigame;
    bool IsPlayerClose = false, IsCooking = false;
   
    public BotController botController;
    public Transform ServiceSpawner;
    public List<GameObject> ListDrink, MinigameList;
    public List<Transform> ListMinigameTranformList;
    public bool IsCookingStarted,IsCookingStartSelecting = false;
    public int DrinkIndex = 0, CookingState;

    public float dPadHorizontal, dPadHorizontal2, dPadHorizontal3;
    private float inputCooldown = 0.2f; // Cooldown time in seconds
    private float lastInputTime = 0f, lastInputTime2 = 0f, lastInputTime3 = 0f;   // Tracks the time of the last D-pad input

    void Start()
    {
        InitializeSystem();
    }

    void Update()
    {
      


       
        if (ClosestPlayer == null)
        {
            StopCookingDueToPlayerExit();
            return;  // Exit the Update loop early to prevent errors
        }
        HandlePlayerProximity();
        HandleCookingProcess();
        
       
    }
    private void StopCookingDueToPlayerExit()
    {
        Debug.Log("Closest player is null, stopping cooking.");
        IsCooking = false;
        IsCookingStarted = false;
        MenuCanvas.SetActive(false);
        Holdindicator.SetActive(true);
        cookingCanvas.SetActive(false);
        botController = null; // Ensure the bot controller is reset
    }
    // Initialize system by disabling UI elements
    public void InitializeSystem()
    {
        cookingCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
        IsCookingStarted = false;
        CookingState = 0;
    }

    // Handle logic when player is near or far
    private void HandlePlayerProximity()
    {
        if (IsPlayerClose)
        {
            ShowCookingCanvas();
        }
        else
        {
            cookingCanvas.SetActive(false);
        }
    }

    // Handle cooking process (i.e., mini-game controls)
    private void HandleCookingProcess()
    {
        if (IsCookingStartSelecting)
        {
            HandleCookingSelection();
        }
        
       

    }

    public void SpawnDrink(int index)
    {
        Instantiate(ListDrink[index], ServiceSpawner.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other) && (ClosestPlayer == null))
        {
            HandlePlayerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other) && (ClosestPlayer != null))
        {
            HandlePlayerExit();
        }
    }

    // Check if the collider belongs to a player
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player1") || other.CompareTag("Player2") || other.CompareTag("Player3");
    }

    // Handle logic when a player enters the trigger area
    private void HandlePlayerEnter(Collider other)
    {
        IsPlayerClose = true;
        ClosestPlayer = other.gameObject;
        botController = ClosestPlayer.GetComponent<BotController>();
       
    }
   

    // Handle logic when a player exits the trigger area
    private void HandlePlayerExit()
    {
        IsPlayerClose = false;
        ClosestPlayer = null;
        if(botController != null)
        {
            botController.enabled = true;
        }
        botController = null;
    }

    // Display the cooking UI when the player is near
    private void ShowCookingCanvas()
    {
        cookingCanvas.SetActive(true);
        Holdindicator.SetActive(true);
       if(ClosestPlayer.tag == "Player1")
        {  
            if(IsCookingStartSelecting == false)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Player1Action"))
                {
                    StartCooking();
                    IsCookingStartSelecting = true;
                    DrinkIndex = 1;
                    CookingState = 1;
                   
                }
            }
            
        }
        if (ClosestPlayer.tag == "Player2")
        {
            if (IsCookingStartSelecting == false)
            {
                if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Player2Action"))
                {
                    StartCooking();
                    IsCookingStartSelecting = true;
                    DrinkIndex = 1;
                    CookingState = 1;

                }
            }
        }
        if (ClosestPlayer.tag == "Player3")
        {
            if (IsCookingStartSelecting == false)
            {
                if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetButtonDown("Player3Action"))
                {
                    StartCooking();
                    IsCookingStartSelecting = true;
                    DrinkIndex = 1;
                    CookingState = 1;

                }
            }
        }

    }

    // Start the cooking process
    private void StartCooking()
    {
        Holdindicator.SetActive(false);
        MenuCanvas.SetActive(true);
        IsCooking = true;
    }

    // Stop the cooking process
    private void StopCooking()
    {
        botController.enabled = true;
        Holdindicator.SetActive(true);
        MenuCanvas.SetActive(false);
        IsCooking = false;
        IsCookingStartSelecting = false;
        
    }
    public void HandleCookingSelection()
    {
        dPadHorizontal = Input.GetAxis("DPadHorizonal1");
        dPadHorizontal2 = Input.GetAxis("DPadHorizonal2");
        dPadHorizontal3 = Input.GetAxis("DPadHorizonal3");
        botController.enabled = false;
        

        if (ClosestPlayer.tag == "Player1")
        {
            if (Time.time - lastInputTime > inputCooldown)
            {

                if ((Input.GetKeyDown(KeyCode.A)|| dPadHorizontal == -1) && DrinkIndex > 1)
                {
                    DrinkIndex--;
                    CookingState = 2;
                     lastInputTime = Time.time; 
                }
                if ((Input.GetKeyDown(KeyCode.D) || dPadHorizontal == 1) && DrinkIndex < ListDrink.Count)
                {   
                    DrinkIndex++;
                    CookingState = 2;
                    lastInputTime = Time.time;
                }
            }
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonUp("Player1Action")) && CookingState >1 )
            {
               
                IsCookingStartSelecting = false;
                IsCooking = true;
                MenuCanvas.SetActive(false);
                HandleSelected();


            }
            else if (CookingState <= 1)
            {
                CookingState = 2;
            }

        }
        if(ClosestPlayer.tag == "Player2")
        {
            if (Time.time - lastInputTime2 > inputCooldown)
            {
                if ((Input.GetKeyDown(KeyCode.H) || dPadHorizontal2 == -1) && DrinkIndex > 1)
                {
                    DrinkIndex--;
                    CookingState = 2;
                    lastInputTime2 = Time.time;
                }
                if ((Input.GetKeyDown(KeyCode.K) || dPadHorizontal2 == 1) && DrinkIndex < ListDrink.Count)
                {
                    DrinkIndex++;
                    CookingState = 2;
                    lastInputTime2 = Time.time;
                }
            }
            if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonUp("Player2Action") && CookingState > 1)
            {

                IsCookingStartSelecting = false;
                IsCooking = true;
                MenuCanvas.SetActive(false);
                HandleSelected();


            }
            else if (CookingState <= 1)
            {
                CookingState = 2;
            }
        }
        if(ClosestPlayer.tag == "Player3")
        {
            if (Time.time - lastInputTime3 > inputCooldown)
            {
                if ((Input.GetKeyDown(KeyCode.Keypad4) || dPadHorizontal3 == -1) && DrinkIndex > 1)
                {
                    DrinkIndex--;
                    CookingState = 2;
                    lastInputTime3 = Time.time;
                }
                if ((Input.GetKeyDown(KeyCode.Keypad6) || dPadHorizontal3 == 1) && DrinkIndex < ListDrink.Count)
                {
                    DrinkIndex++;
                    CookingState = 2;
                    lastInputTime3 = Time.time;
                }
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetButtonUp("Player3Action") && CookingState > 1)
            {
                IsCookingStartSelecting = false;
                IsCooking = true;
                MenuCanvas.SetActive(false);
                HandleSelected();
            }
            else if (CookingState <= 1)
            {
                CookingState = 2;
            }
        }
    }
    public void HandleSelected()
    {
        if (IsCooking )
        {
            if (!IsCookingStarted)
            {
                if (DrinkIndex == 1)
                {
                    IsCookingStarted = true;
                    PlayMinigameDrinkmaking(0);
                }
                else if (DrinkIndex == 2)
                {
                    IsCookingStarted = true;
                    PlayMinigameDrinkmaking(1);
                }
                else if (DrinkIndex == 3)
                {
                    IsCookingStarted = true;
                    PlayMinigameDrinkmaking(2);
                }
                else
                {
                    StopCooking();
                }
            }

        }
    }

    // Play the drink-making mini-game
    public void PlayMinigameDrinkmaking(int minigamenum)
    {
        bool IsMakingadrinkfin = false;

        if (!IsMakingadrinkfin)
        {
            GameObject minigametoSpawned = Instantiate(MinigameList[0], ListMinigameTranformList[0].position, ListMinigameTranformList[0].rotation);

            RectTransform rectTransform = minigametoSpawned.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                minigametoSpawned.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            botController.enabled = false;
            CurrentMinigame = minigametoSpawned;
            StartCoroutine(MonitorMinigame(minigametoSpawned, minigamenum, IsMakingadrinkfin));
        }
        
    }

    // Monitor the mini-game and spawn the drink upon completion
    private IEnumerator MonitorMinigame(GameObject minigame, int drinkIndex, bool IsMakingadrinkfin)
    {
        DrinkmakingMinigame minigameScript = minigame.GetComponent<DrinkmakingMinigame>();
      
        while (!minigameScript.Isgamefin)
        {
            Holdindicator.SetActive(false);
            yield return null;
        }

        SpawnDrink(drinkIndex);
        botController.enabled = true;
        CurrentMinigame = null;
        Destroy(minigame);
        IsCookingStarted = false;
        
    }
}
