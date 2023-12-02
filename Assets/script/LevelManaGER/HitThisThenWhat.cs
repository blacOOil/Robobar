using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitThisThenWhat : MonoBehaviour
{
    public GameObject Hitthis, Then;
    // Start is called before the first frame update
    void Start()
    {
        Then.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Then.SetActive(true);
            Destroy(gameObject);
        }
    }
}
