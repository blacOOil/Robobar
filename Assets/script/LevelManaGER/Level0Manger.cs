using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level0Manger : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] TextMeshProUGUI RemainingOBJ,Timetext;
    [SerializeField] public float remainingTime;
    public float Dropped = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RemainingOBJ.text = string.Format("{0}", Dropped);
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        Timetext.text = string.Format("{0:00}:{1:00}", minutes, second);

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("TargetTag"))
        {
            Dropped--;
            RemainingOBJ.text = Dropped.ToString();
        }
    }
}
