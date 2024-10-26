using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingSystem : MonoBehaviour
{
    public GameObject cookingCanvas, MenuCanvas, ClosestPlayer, Holdindicator;
    bool IsPlayerClose = false, IsCooking = false;
    public BotController botController;
    public Transform ServiceSpawner;
    public List<GameObject> ListDrink, MinigameList;
    public List<Transform> ListMinigameTranformList;
    public bool IsCookingStarted,IsCookingStartSelecting = false;
    public int DrinkIndex = 0;
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
    private void InitializeSystem()
    {
        cookingCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
        IsCookingStarted = false;
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
        if (IsCooking)
        {

            if (ClosestPlayer.tag == "Player1")
            {
                if (!IsCookingStarted)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(2);
                    }
                }
                
            }
            if (ClosestPlayer.tag == "Player2")
            {
                if (!IsCookingStarted)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha7))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha8))
                    {
                        IsCookingStarted = true;
                        PlayMinigameDrinkmaking(2);
                    }
                }
            }
            if(IsCookingStartSelecting == true)
            {
                HandleCookingSelection();
            }
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
        return other.CompareTag("Player1") || other.CompareTag("Player2");
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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCooking();
                    IsCookingStartSelecting = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StopCooking();
                    IsCookingStartSelecting = false;
                }
            }
        }
        if (ClosestPlayer.tag == "Player2")
        {
            if (Input.GetKey(KeyCode.I))
            {
                StartCooking();
            }

            if (Input.GetKeyUp(KeyCode.I))
            {
                StopCooking();
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
        botController.enabled = false;
    }

    // Play the drink-making mini-game
    public void PlayMinigameDrinkmaking(int minigamenum)
    {
        bool IsMakingadrinkfin = false;

        if (!IsMakingadrinkfin)
        {
            GameObject minigametoSpawned = Instantiate(MinigameList[0], ListMinigameTranformList[0].position, ListMinigameTranformList[0].rotation);
            botController.enabled = false;

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
        Destroy(minigame);
        IsCookingStarted = false;
    }
}
