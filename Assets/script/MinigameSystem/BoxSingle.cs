using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSingle : MonoBehaviour
{
    public Transform spawnPoint;
    public float CheckerRadius = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturntoSpawnPoint()
    {
        gameObject.transform.position = spawnPoint.position;
    }
}
