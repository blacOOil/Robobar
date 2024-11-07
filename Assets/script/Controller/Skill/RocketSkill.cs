using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSkill : MonoBehaviour
{
    public BotController botController;
    public float additionSpeed;
    public bool Isadded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Isadded)
        {
            botController.Speed += additionSpeed;
            Isadded = true;
        }
    }
}
