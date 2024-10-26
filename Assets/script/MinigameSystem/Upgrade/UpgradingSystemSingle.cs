using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSystemSingle : MonoBehaviour
{
    public GameObject CanvasUpgrade, CloestPlayer;
    public BotController botController;
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

        if (CloestPlayer != null)
        {
            if (CloestPlayer.tag == "Player1" && Input.GetKey(KeyCode.E))
            {
                return true;
            }
            else if (CloestPlayer.tag == "Player2" && Input.GetKey(KeyCode.U))
            {
                return true;
            }
        }

        return false;
    }

    void Start()
    {
    }

    void Update()
    {
        if (CheckPlayer())
        {
            CanvasUpgrade.SetActive(true);
            if (PlayerInputCheck())
            {
      
                UpgradingProcess();
            }else if(botController != null) 
            {
                botController.enabled = true;
                botController = null;
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

        CloestPlayer = closestPlayer;
    }

    public void UpgradingProcess()
    {
        botController = CloestPlayer.GetComponent<BotController>();
        botController.enabled = false;

        if (CloestPlayer.tag == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.A) && MaterialIndex > 0)
            {
                MaterialIndex--;
            }
            if (Input.GetKeyDown(KeyCode.D) && MaterialIndex < ChairMaterial.Count - 1)
            {
                MaterialIndex++;
            }
            botController = CloestPlayer.GetComponent<BotController>();
        }
        else if (CloestPlayer.tag == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.H) && MaterialIndex > 0)
            {
                MaterialIndex--;
            }
            if (Input.GetKeyDown(KeyCode.K) && MaterialIndex < ChairMaterial.Count - 1)
            {
                MaterialIndex++;
            }
        }

        // Apply material after updating the MaterialIndex
        ApplyMaterialToSeats();
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
