using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1StartCutScene : MonoBehaviour
{
    public GameObject StartingText,Player;
    private Rigidbody PlayerRigid;
    
    // Start is called before the first frame update
    void Start()
    {
        StartingText.SetActive(true);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(5);
    }
}
