using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CutSceneEnter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public CinemachineVirtualCamera CutSceneCam,PlaerCam;
    public float cutscenduration = 0f;
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
        CutSceneCam.Priority = 0;
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
           
            CutSceneCam.Priority =3;
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            BackToGame();
           
        }

    }
    IEnumerator fincut()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Wait Successful");
    }
    public void BackToGame()
    {
        StartCoroutine(fincut());
       // CutSceneCam.SetActive(false);
       // PlaerCam.SetActive(true);
        playerRigidbody.constraints = RigidbodyConstraints.None;

    }
   

   
}
