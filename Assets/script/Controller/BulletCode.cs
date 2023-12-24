using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode : MonoBehaviour
{
    public float timeToLiveBullets;

    void Start()
    {
        Destroy(gameObject, timeToLiveBullets); ;
    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has collided with an enemy.
        if (other.CompareTag("ENEMY"))
        {
            Destroy(gameObject);

        }
    }
}
