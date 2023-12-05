using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipperPoint : MonoBehaviour
{

    public Transform Player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == Player.position)
        {
            Shooting();
        }
        transform.position = Vector3.Lerp(transform.position,Player.position, speed * Time.deltaTime);
    }
    void Shooting()
    {

    }
}
