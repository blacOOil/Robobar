using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotController : MonoBehaviour
{
    [Header("Movement Controller")]
    public float Speed;
    public float JumpForce;
    public float GroundCheckDistance = 0.1f;
    public LayerMask GroundLayer;
    public Light Playerlight;

    private Camera mainCamera;
    private Rigidbody rb;
    private bool isGrounded;

    [Header("Name Indicator")]
    [SerializeField] public string inputNameHorizontal;
    [SerializeField] public string inputNameVertical;

    
    private string PlayerName;
    public GameObject NameObj;
    public PlayerNameContainer playerNameContainer;
    public TMP_Text NameText;
    

    [Header("Animation Controller")]
    public float animSpeedMultiplier;
    Animator anim;
    
    private List<string> assignedJoysticks = new List<string>();
    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        NameObj.SetActive(true);
        AssignJoystick();
    }

    void Update() {

        UpdatePlayerName();
        MovementInput();
        JumpInput();
        CheckGroundStatus();
        PlayerIndicator();
        UpdateJoyStickInput();
    }
    void AssignJoystick()
    {
     
            string[] joysticks = Input.GetJoystickNames();

            if (joysticks.Length > 0)
            {
                assignedJoysticks.Clear(); // Clear the list to avoid duplicate entries

                for (int i = 0; i < joysticks.Length; i++)
                {
                    if (!string.IsNullOrEmpty(joysticks[i]))
                    {
                        string joystickName = "Joystick" + (i + 1);
                        assignedJoysticks.Add(joystickName); // Add the joystick to the list
                        Debug.Log($"Assigned {joysticks[i]} to {joystickName}");
                    }
                }

                Debug.Log($"Total assigned joysticks: {assignedJoysticks.Count}");
            }
            else
            {
                Debug.LogWarning("No joystick connected!");
            }
       
    }
    void UpdateJoyStickInput()
    {
        if (assignedJoysticks.Count >= 1 && inputNameHorizontal == "Horizontal1")
        {
            inputNameHorizontal = "Joystick1Horizontal1";
            inputNameVertical = "Joystick1Vertical1";
        }

        if (assignedJoysticks.Count >= 2 && inputNameHorizontal == "Horizontal2")
        {
            inputNameHorizontal = "Joystick2Horizontal1";
            inputNameVertical = "Joystick2Vertical1";
        }

        if (assignedJoysticks.Count >= 3 && inputNameHorizontal == "Horizontal3")
        {
            inputNameHorizontal = "Joystick3Horizontal1";
            inputNameVertical = "Joystick3Vertical1";
        }
    }
    void MovementInput()
    {
        
        float horizontalInput = Input.GetAxis(inputNameHorizontal);
        float verticalInput = Input.GetAxis(inputNameVertical);

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction in world space
        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        if (movementDirection.magnitude >= 0.1f)
        {
            // Apply movement using Rigidbody
            Vector3 newVelocity = new Vector3(movementDirection.x * Speed, rb.velocity.y, movementDirection.z * Speed);
            rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.deltaTime * 10f); // Smooth interpolation

            // Calculate the rotation to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation

            // Handle jump input
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }

            anim.GetComponent<Animator>().SetBool("IsWalk", true);
            anim.SetFloat("Anim Speed", animSpeedMultiplier);
        } else {
            anim.GetComponent<Animator>().SetBool("IsWalk", false);    
        }
    }

    void JumpInput() {
        // Handle jump input
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void CheckGroundStatus() {
        // Perform a raycast downwards to check if the bot is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance, GroundLayer);
    }

    private void OnDrawGizmos() {
        // Draw a ray in the editor to visualize the ground check
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * GroundCheckDistance);
    }
    public void PlayerIndicator()
    {
        if(inputNameHorizontal == null)
        {
            Playerlight.color = Color.white;
        }else if(inputNameHorizontal == "Horizontal2" || inputNameHorizontal == "Joystick2Horizontal1")
        {
            Playerlight.color = Color.blue;
        }else if(inputNameHorizontal == "Horizontal3")
        {
            Playerlight.color = Color.green;
        }
        else
        {
            Playerlight.color = Color.red;
        }
    }
    public void UpdatePlayerName()
    {
        playerNameContainer = GameObject.Find("LevelManager").GetComponent<PlayerNameContainer>();
        UpdatePlayerTag();
       
    }
    public void UpdatePlayerTag()
    {
        if(gameObject.tag == "Player1")
        {
            PlayerName = playerNameContainer.PlayerNameList[0];
            NameText.text = PlayerName;
        }
        if (gameObject.tag == "Player2")
        {
            PlayerName = playerNameContainer.PlayerNameList[1];
            NameText.text = PlayerName;
        }
        if (gameObject.tag == "Player3")
        {
            PlayerName = playerNameContainer.PlayerNameList[2];
            NameText.text = PlayerName;
        }
    }
}
