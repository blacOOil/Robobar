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

    private float inputCooldown = 0.2f; // Time in seconds between inputs
    private float lastInputTime = 0f;   // Tracks the time of the last input
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

        if (ShakingCoustiing > 5)
        {

            IsShakingfin = true;

        }
        else
        {
            if (ClosetPlayer.tag == "Player1")
            {
                float dPadHorizontal = Input.GetAxisRaw("Joystick1Horizontal1");
                if (Time.time - lastInputTime > inputCooldown)
                {
                    if (!isapressing)
                    {
                        if (Input.GetKeyDown(KeyCode.A) || dPadHorizontal == -1)
                        {
                            ShakingCoustiing++;
                            isapressing = true;
                            isdpressing = false;
                            lastInputTime = Time.time;
                        }
                    }
                    if (!isdpressing)
                    {
                        if (Input.GetKeyDown(KeyCode.D) || dPadHorizontal == -1)
                        {
                            ShakingCoustiing++;
                            isapressing = false;
                            isdpressing = true;
                            lastInputTime = Time.time;
                        }
                    }
                }
            }
            if (ClosetPlayer.tag == "Player2")
            {
                float dPadHorizontal2 = Input.GetAxisRaw("Joystick2Horizontal1");
                if (Time.time - lastInputTime > inputCooldown)
                {
                    if (!isapressing)
                    {
                        if (Input.GetKeyDown(KeyCode.H) || dPadHorizontal2 == -1)
                        {
                            ShakingCoustiing++;
                            isapressing = true;
                            isdpressing = false;
                        }
                    }
                    if (!isdpressing)
                    {
                        if (Input.GetKeyDown(KeyCode.K)|| dPadHorizontal2 == 1)
                        {
                            ShakingCoustiing++;
                            isapressing = false;
                            isdpressing = true;
                        }
                    }
                }

            }
            if (ClosetPlayer.tag == "Player3")
            {
                float dPadHorizontal3 = Input.GetAxisRaw("Joystick3Horizontal1");
                if (Time.time - lastInputTime > inputCooldown)
                {
                    if (!isapressing)
                    {
                        if (Input.GetKeyDown(KeyCode.Keypad4) || dPadHorizontal3 == -1)
                        {
                            ShakingCoustiing++;
                            isapressing = true;
                            isdpressing = false;
                        }
                    }
                    if (!isdpressing)
                    {
                        if (Input.GetKeyDown(KeyCode.Keypad6) || dPadHorizontal3 == 1)
                        {
                            ShakingCoustiing++;
                            isapressing = false;
                            isdpressing = true;
                        }
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
        GameObject[] player3Objects = GameObject.FindGameObjectsWithTag("Player3");

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
        foreach (GameObject player3 in player3Objects)
        {
            float distance = Vector3.Distance(transform.position, player3.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                tempClosest = player3;
            }
        }

        // Set the closest player
        ClosetPlayer = tempClosest;
    }
}


