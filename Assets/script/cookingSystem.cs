using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookingSystem : MonoBehaviour
{
    public GameObject cookingCanvas, MenuCanvas,ClosestPlayer,Holdindicator;
    bool IsPlayerClose = false, IsCooking = false;
    public BotController botController;
    public Transform ServiceSpawner;
    public List<GameObject> ListDrink,MinigameList;
    public List<Transform> ListMinigameTranformList;

    

    // Start is called before the first frame update
    void Start()
    {
        cookingCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
    
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerClose == true)
        {
            PlayerISClose();
        }
        else
        {
            cookingCanvas.SetActive(false);
        }
        if (IsCooking)
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
    }
    public void SpawnDrink(int index)
    {
        Instantiate(ListDrink[index], ServiceSpawner.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerClose = true;
            ClosestPlayer = other.gameObject;
            botController = ClosestPlayer.GetComponent<BotController>();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerClose = false;
            ClosestPlayer = null;
            botController = null;
        }

    }
    public void PlayerISClose()
    {
        cookingCanvas.SetActive(true);
        Holdindicator.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            Holdindicator.SetActive(false);
            MenuCanvas.SetActive(true);
            IsCooking = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Holdindicator.SetActive(true);
            MenuCanvas.SetActive(false);
            IsCooking = false;
        }

    }
   public void PlayMinigameDrinkmaking(int minigamenum)
    {
        
        GameObject minigametoSpawned;
         bool IsMakingadrinkfin = false;
        if (!IsMakingadrinkfin)
        {
            minigametoSpawned = Instantiate(MinigameList[0], ListMinigameTranformList[0].position, ListMinigameTranformList[0].rotation);
            botController.enabled = false;
            StartCoroutine(MonitorMinigame(minigametoSpawned, minigamenum,IsMakingadrinkfin));

        }
       
    }
    private IEnumerator MonitorMinigame(GameObject minigame, int drinkIndex,bool IsMakingadrinkfin)
    {
        // Get the DrinkmakingMinigame component
        DrinkmakingMinigame minigameScript = minigame.GetComponent<DrinkmakingMinigame>();

        // Wait until the minigame is finished
        while (!minigameScript.Isgamefin)
        {
            Holdindicator.SetActive(false);
            yield return null; // Wait until the next frame

        }

        // Once the minigame is finished, spawn the drink
       
        SpawnDrink(drinkIndex);
        botController.enabled = true;
        Destroy(minigame);
    }
}
