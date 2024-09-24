using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkmakingMinigame : MonoBehaviour
{
    public float Alimit, Dlitmit,PassCenterAmount,speed = 10f;
    public GameObject drink,shakingGroup,Player;
    public Transform CentPivot;
    public bool IsShakingfin, Isgamefin;
   public int ShakingCoustiing = 0;
    public bool isapressing = false;
   public bool isdpressing = false;
    public float searchRadius = 900000f;
    // Start is called before the first frame update
    void Start()
    {
        PassCenterAmount = 0;
        IsShakingfin = false;
        Isgamefin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Isgamefin)
        {
            if (!IsShakingfin)
            {
                shakingGroup.SetActive(true);
                Shaking();
            }
            else
            {
              //  shakingGroup.SetActive(false);
                Isgamefin = true;
            }
        }
        else
        {
            
        }

        
    }
    public void ShakingCouting()
    {
        
        if(ShakingCoustiing > 5)
        {
            
            IsShakingfin = true;
        
        }
        else
        {

            if (!isapressing)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    ShakingCoustiing++;
                    isapressing = true;
                    isdpressing = false;
                }
            }
            if (!isdpressing)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    ShakingCoustiing++;
                    isapressing = false;
                    isdpressing = true;
                }
            }
           
        }
       
    }

    public void Shaking()
    {
        float moveInput = Input.GetAxis("Horizontal1");
        drink.transform.Translate(Vector3.right * moveInput * speed * Time.deltaTime);
        if (moveInput == 0)
        {
            drink.transform.position = Vector3.Lerp(drink.transform.position, CentPivot.position, speed * Time.deltaTime);
        }
        ShakingCouting();
    }
    public void findClosestPlayer()
    {
        float closestDistance = Mathf.Infinity;
        GameObject ClosestPlayer = null;

        // Find all objects tagged with Player1 and Player2
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player2");

        // Combine both Player1 and Player2 into one array for easier processing
        List<GameObject> allPlayers = new List<GameObject>();
        allPlayers.AddRange(players);
        allPlayers.AddRange(players2);

        // Loop through all players
        foreach (GameObject player in allPlayers)
        {
            ChairSingle chairSingle = player.GetComponent<ChairSingle>();
            if (chairSingle != null && !chairSingle.isSited)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestDistance && distance <= searchRadius)
                {
                    closestDistance = distance;
                    ClosestPlayer = player;
                }
            }
        }

        if (ClosestPlayer != null)
        {
            ChairSingle closestChairSingle = ClosestPlayer.GetComponent<ChairSingle>();
            if (closestChairSingle != null)
            {
                closestChairSingle.isSited = true; // Mark the player as seated
            }
            Player = ClosestPlayer; // Assign the closest player
        }
    }

}
