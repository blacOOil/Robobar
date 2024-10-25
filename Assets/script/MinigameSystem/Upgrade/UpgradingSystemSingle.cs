using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSystemSingle : MonoBehaviour
{
    public GameObject CanvasUpgrade, CloestPlayer;
    public List<GameObject> SeatInSider;
    public List<Material> ChairMaterial;
    public List<Material> TableMaterial;
    private float PlayerCheckerRadius = 3f;
    private int MaterialIndex = 0;
    private bool CheckPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player1") || hitCollider.CompareTag("Player2"))
            {

                return true;
            }
        }
        return false;
    }
    public bool PlayerInputCheck()
    {
        CheckClosetPlayer();

        // Check if CloestPlayer exists before accessing its tag
        if (CloestPlayer != null)
        {
            if (CloestPlayer.tag == "Player1")
            {
                if (Input.GetKey(KeyCode.E))  // Use Input.GetKeyDown for KeyCode
                {
                    return true;
                }
            }
            else if (CloestPlayer.tag == "Player2")
            {
                if (Input.GetKey(KeyCode.U))  // Use Input.GetKeyDown for KeyCode
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPlayer())
        {
            CanvasUpgrade.SetActive(true);
            if (PlayerInputCheck())
            {
                UpgradingProcess();
            }
        }
        else
        {
            CanvasUpgrade.SetActive(false);
            CloestPlayer = null;
        }
    }
    public void CheckClosetPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player1") || hitCollider.CompareTag("Player2"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = hitCollider.gameObject;
                }
            }
        }

        // Set the closest player
        CloestPlayer = closestPlayer;
    }
    public void UpgradingProcess()
    {
        if (CloestPlayer.tag == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (MaterialIndex !<= 0)
                {
                    MaterialIndex--;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(MaterialIndex! >= ChairMaterial.Count)
                {
                    MaterialIndex++;
                }
            }
            ApplyMaterialToSeats();
        }
        if (CloestPlayer.tag == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (MaterialIndex! <= 0)
                {
                    MaterialIndex--;
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (MaterialIndex! >= ChairMaterial.Count)
                {
                    MaterialIndex++;
                }
            }
        }

    }
    private void ApplyMaterialToSeats()
    {
        foreach (var seat in SeatInSider)
        {
            Renderer renderer = seat.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = ChairMaterial[MaterialIndex];
            }
        }
    }
}
