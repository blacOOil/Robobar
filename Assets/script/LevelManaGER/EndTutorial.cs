using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    public GameObject TutorialCam, MainCam;
    public Transform MainGamePosition, Player;
    // Start is called before the first frame update
    void Start()
    {
        MainCam.SetActive(false);
        Vector3 Spawner = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialCam.SetActive(false);
            MainCam.SetActive(true);
            Player.position = MainGamePosition.position;
            Destroy(gameObject);
        }
    }
}
