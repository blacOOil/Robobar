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

    void Start()
    {
        InitializeSystem();
    }

    void Update()
    {
        HandlePlayerProximity();
        HandleCookingProcess();
    }

    // Initialize system by disabling UI elements
    private void InitializeSystem()
    {
        cookingCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
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
               
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        PlayMinigameDrinkmaking(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        PlayMinigameDrinkmaking(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        PlayMinigameDrinkmaking(2);
                    }
                
            }
            if (ClosestPlayer.tag == "Player2")
            {
                
                    if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        PlayMinigameDrinkmaking(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha7))
                    {
                        PlayMinigameDrinkmaking(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha8))
                    {
                        PlayMinigameDrinkmaking(2);
                    }
                
            }
        }
       

    }

    public void SpawnDrink(int index)
    {
        Instantiate(ListDrink[index], ServiceSpawner.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            HandlePlayerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
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
        botController = null;
    }

    // Display the cooking UI when the player is near
    private void ShowCookingCanvas()
    {
        cookingCanvas.SetActive(true);
        Holdindicator.SetActive(true);
       if(ClosestPlayer.tag == "Player1")
        {
            if (Input.GetKey(KeyCode.E))
            {
                StartCooking();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                StopCooking();
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
        Holdindicator.SetActive(true);
        MenuCanvas.SetActive(false);
        IsCooking = false;
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
    }
}
