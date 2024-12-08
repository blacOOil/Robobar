using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSingle : MonoBehaviour
{
    public int DrinkId;
    private float PlayerCheckerRadius = 1;
    public GameObject Self, ClosestPlayer;
    public bool IsHeld;
    private float despawnTime = 10500f;

    private bool CheckPlayer()
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player1")|| hitCollider.CompareTag("Player2")|| hitCollider.CompareTag("Player3"))
            {
                return true;
            }
        }
        return false;
    }

    private bool Isground()
    {
        // Check all layers to capture all objects tagged with the given tag
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Ground"))
            {
                return true;
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
        if(IsHeld == true)
        {
            CheckPlayerClosed();
            StopCoroutine(DespawnCountdown());
        }
        if(!IsHeld && Isground())
        {
            StartCoroutine(DespawnCountdown());
        }
        else
        {
            StopCoroutine(DespawnCountdown());
        }
        
    }
    public void selfDestruct()
    {
        Destroy(Self);
    }
    public void CheckPlayerClosed()
    {
        StartCoroutine(DelayChecking());
        if (!CheckPlayer())
        {
            IsHeld = false;
        }
    }
    IEnumerator DelayChecking()
    {
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator DespawnCountdown()
    {
        Debug.Log("Countdown started!");
        yield return new WaitForSeconds(despawnTime); // Wait for the specified time
        if (!IsHeld)
        {
            Destroy(gameObject); // Destroy the object
        }
      
    }
}
