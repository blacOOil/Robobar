using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public List<Transform> PlayerSpawnedPointTransform;
    public float minx, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randdomposition = new Vector2(Random.Range(minx, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(PlayerPrefab.name, randdomposition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
