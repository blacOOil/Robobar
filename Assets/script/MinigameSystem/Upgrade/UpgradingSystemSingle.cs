using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSystemSingle : MonoBehaviour
{
    public GameObject CanvasUpgrade, CloestPlayer;
    public MonneyLevelCode monneyLevelCode;
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
            if (hitCollider.CompareTag("Player1") || hitCollider.CompareTag("Player2")|| hitCollider.CompareTag("Player3"))
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
            if (CloestPlayer.tag == "Player1" &&( Input.GetKey(KeyCode.E) || Input.GetButtonDown("Player1Action")))
            {
                return true;
            }
            else if (CloestPlayer.tag == "Player2" && Input.GetKey(KeyCode.U) || Input.GetButtonDown("Player2Action"))
            {
                return true;
            }
            else if (CloestPlayer.tag == "Player3" && Input.GetKey(KeyCode.Keypad9) || Input.GetButtonDown("Player3Action"))
            {
                return true;
            }
        }

        return false;
    }

    void Start()
    {
        monneyLevelCode = GameObject.Find("LevelManager").GetComponent<MonneyLevelCode>();
    }

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
            if (hitCollider.CompareTag("Player1") || hitCollider.CompareTag("Player2") || hitCollider.CompareTag("Player3"))
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
        if ((MaterialIndex < ChairMaterial.Count - 1) && monneyLevelCode.MonneyAmount >0)
        {
            MaterialIndex++;
            monneyLevelCode.MonneyAmount--;
        }
        // Apply material after updating the MaterialIndex
        ApplyMaterialToSeats();
    }

    private void ApplyMaterialToSeats()
    {
        if (MaterialIndex >= 0 && MaterialIndex < ChairMaterial.Count)
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
        else
        {
            Debug.LogWarning("MaterialIndex is out of range. Check your ChairMaterial list.");
        }
    }
}
