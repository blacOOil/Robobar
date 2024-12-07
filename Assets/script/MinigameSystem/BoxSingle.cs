using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSingle : MonoBehaviour
{
    public Transform spawnPoint;
    public float CheckerRadius = 4f;
    public GameObject PLacementCanvas;
    public CrossingRoadManager crossingRoadManager;

    private bool isPlayerScored; // To prevent repeated score increments
    // Start is called before the first frame update
    void Start()
    {
        PLacementCanvas.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerClose())
        {
            PLacementCanvas.SetActive(true);
        }
        else
        {
            isPlayerScored = false;
            //  PLacementCanvas.SetActive(false);
        }
    }
    public void ReturntoSpawnPoint()
    {
        gameObject.transform.position = spawnPoint.position;
    }
    public bool IsPlayerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,4f);
        foreach (var hitCollider in hitColliders)
        {
            if ((gameObject.tag == "Box1" && hitCollider.CompareTag("Player1")))
            {
                if(isPlayerScored == false)
                {
                    crossingRoadManager.PlayerScoreint[0]++;
                    isPlayerScored = true;
                }
                
                return true;
            }
            if ((gameObject.tag == "Box2" && hitCollider.CompareTag("Player2")))
            {
                if (isPlayerScored == false)
                {
                    crossingRoadManager.PlayerScoreint[1]++;
                    isPlayerScored = true;
                }
                return true;
            }
            if ((gameObject.tag == "Box3" && hitCollider.CompareTag("Player3")))
            {
                if (isPlayerScored == false)
                {
                    crossingRoadManager.PlayerScoreint[2]++;
                    isPlayerScored = true;
                }
                return true;
            }
        }
        return false;
    }
   

}
