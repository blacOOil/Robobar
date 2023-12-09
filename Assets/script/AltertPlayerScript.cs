using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AltertPlayerScript : MonoBehaviour
{
    public GameObject Player,AltertCams;
    public TextMeshProUGUI ObjectiveText;
    public Rigidbody PlayerRigid;
   
        // Start is called before the first frame update
    void Start()
    {
        AltertCams.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AltertCams.SetActive(true);
            StartAlertScene();
            AltertCams.SetActive(false);
        }    
    }

    IEnumerator StartAlertScene()
    {
        yield return new WaitForSeconds(4);
    }
}
