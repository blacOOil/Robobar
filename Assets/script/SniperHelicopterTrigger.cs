using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHelicopterTrigger : MonoBehaviour
{
    public GameObject Player, Helicopter, sniperPoint;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
        Helicopter.SetActive(false);
        sniperPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            Helicopter.SetActive(true);
            sniperPoint.SetActive(true);
            HelicopterCutscene();
            playerRigidbody.constraints = RigidbodyConstraints.None;
            Destroy(gameObject);


        }
        
    }
    IEnumerator HelicopterCutscene()
    {
        yield return new WaitForSeconds(4);
    }
}
