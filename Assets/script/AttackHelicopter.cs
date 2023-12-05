using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHelicopter : MonoBehaviour
{
    public Transform Player;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position, Speed * Time.deltaTime);
    }
}
