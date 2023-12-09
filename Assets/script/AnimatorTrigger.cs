using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    [SerializeField] public Animator CutScene;
    public float cutscenduration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Action());
    }
    IEnumerator Action()
    {
        CutScene.SetBool("IsCutSceneStart", true);
        yield return new WaitForSeconds(cutscenduration);
        CutScene.SetBool("IsCutSceneStart", false);
    }
}
