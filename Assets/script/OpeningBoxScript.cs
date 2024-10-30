using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningBoxScript : MonoBehaviour
{
    public Gamestate gamestate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player1")) || other.CompareTag("Player2") || other.CompareTag("Player3"))
        {
            gamestate.please_next();
        }
    }
}
