using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBotController : MonoBehaviour
{
    public CharacterController Controller;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            Controller.Move(direction * speed * Time.deltaTime);
        }

    }
}
