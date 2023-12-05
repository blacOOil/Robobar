using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnterAndChangCam : MonoBehaviour
{
    public GameObject Player, CutSceneCam, PlaerCam,SecondCam;
    public float cutscenduration = 0f;
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
        SecondCam.gameObject.SetActive(false);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine(fincut());

        }

    }
    IEnumerator fincut()
    {
        Debug.Log("startScene");
        PlaerCam.gameObject.SetActive(false);
        CutSceneCam.gameObject.SetActive(true);
        Debug.Log("changeCam");
        yield return new WaitForSeconds(4);
        Debug.Log("EndScene");
        SecondCam.gameObject.SetActive(true);
        CutSceneCam.gameObject.SetActive(false);
        playerRigidbody.constraints = RigidbodyConstraints.None;
       
        Destroy(gameObject);
    }
}
