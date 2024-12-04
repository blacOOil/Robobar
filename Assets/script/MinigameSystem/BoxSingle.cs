using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSingle : MonoBehaviour
{
    public Transform spawnPoint;
    public float CheckerRadius = 4f;
    public GameObject PLacementCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
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
            PLacementCanvas.SetActive(false);
        }
    }
    public void ReturntoSpawnPoint()
    {
        gameObject.transform.position = spawnPoint.position;
    }
    public bool IsPlayerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,2.5f);
        foreach (var hitCollider in hitColliders)
        {
            if ((gameObject.tag == "Box1" && hitCollider.CompareTag("Player1")))
            {
                return true;
            }
            if ((gameObject.tag == "Box2" && hitCollider.CompareTag("Player2")))
            {
                return true;
            }
            if ((gameObject.tag == "Box3" && hitCollider.CompareTag("Player3")))
            {
                return true;
            }
        }
        return false;
    }
   
}
