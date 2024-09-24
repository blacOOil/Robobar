using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkmakingMinigame : MonoBehaviour
{
    public float Alimit, Dlitmit,PassCenterAmount,speed = 10f;
    public GameObject drink,shakingGroup,ClosetPlayer;
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
        FindClosestPlayerWithTag();
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
            if(ClosetPlayer.tag == "Player1")
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
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        ShakingCoustiing++;
                        isapressing = false;
                        isdpressing = true;
                    }
                }
            }
            if (ClosetPlayer.tag == "Player2")
            {
                if (!isapressing)
                {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    ShakingCoustiing++;
                    isapressing = true;
                    isdpressing = false;
                }
                 }  
            if (!isdpressing)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    ShakingCoustiing++;
                    isapressing = false;
                    isdpressing = true;
                }
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
    void FindClosestPlayerWithTag()
    {
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player2");

        float closestDistance = Mathf.Infinity;
        GameObject tempClosest = null;

        // Check player1 objects
        foreach (GameObject player1 in player1Objects)
        {
            float distance = Vector3.Distance(transform.position, player1.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                tempClosest = player1;
            }
        }

        // Check player2 objects
        foreach (GameObject player2 in player2Objects)
        {
            float distance = Vector3.Distance(transform.position, player2.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                tempClosest = player2;
            }
        }

        // Set the closest player
        ClosetPlayer = tempClosest;
    }
}


