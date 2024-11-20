using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkSingle : MonoBehaviour
{
    public int DrinkId;
    private float PlayerCheckerRadius = 1;
    public GameObject Self, ClosestPlayer;
    public bool IsHeld;

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
}
