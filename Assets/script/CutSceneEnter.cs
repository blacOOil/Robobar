using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player, CutSceneCam, PlaerCam;
    public float cutscenduration = 0f;
    private Rigidbody playerRigidbody;

    [SerializeField] public Animator SecondCutScene;

    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
       
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
        SecondCutScene.SetBool("IsCutSceneStart", true);
        yield return new WaitForSeconds(4);
        Debug.Log("EndScene");
        PlaerCam.gameObject.SetActive(true);

        playerRigidbody.constraints = RigidbodyConstraints.None;
        CutSceneCam.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}