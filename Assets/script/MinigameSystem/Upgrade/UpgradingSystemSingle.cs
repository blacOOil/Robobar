using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSystemSingle : MonoBehaviour
{
    public GameObject CanvasUpgrade;
    private float PlayerCheckerRadius = 3f;
    private bool CheckPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, PlayerCheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player1")|| hitCollider.CompareTag("Player2"))
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
        if (CheckPlayer())
        {
            CanvasUpgrade.SetActive(true);

        }
        else
        {
            CanvasUpgrade.SetActive(false);
        }
    }
}
