using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level0Manger : MonoBehaviour
{
    public GameObject Player,EndScreen,MissionFail;
    [SerializeField] TextMeshProUGUI RemainingOBJ,Timetext;
    [SerializeField] public float remainingTime;
    public float Dropped ;
    public float TargetPoint;
    private bool IsFlip = false;
    public BoxCollider Carcollider;
     
    // Start is called before the first frame update
    void Start()
    {
        EndScreen.SetActive(false);
        MissionFail.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        //RemainingOBJ.text = string.Format("{0}", Dropped);
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        Timetext.text = string.Format("{0:00}:{1:00}", minutes, second);

        if(IsFlip == true)
        {
            RemainingOBJ.text = "Hold R to Restart";
        }
        else if(Dropped == 0)
        {
            RemainingOBJ.text = "Pick Up a Client";
        }
        else if (Dropped == 1)
        {
            RemainingOBJ.text = "Avoid The Spottlight";
        }
        else if (Dropped == 2)
        {
            RemainingOBJ.text = "Escape The Snipper";
        }

        else if (Dropped >= TargetPoint)
        {
            EndLevel();
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("TargetTag"))
        {
           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetTag"))
        {
            Dropped++;
            RemainingOBJ.text = Dropped.ToString();
        }
        if (other.CompareTag("InstanceSpotlightTag"))
        {
            Fail();
        }
       
        if (Carcollider.CompareTag("Road"))
        {
            IsFlip = true;
        }
    }
    void EndLevel()
    {
        EndScreen.SetActive(true);
        pause();
    }
    void pause()
    {
        Time.timeScale = 0;
    }
    void Fail()
    {
        WaitForProgress();
        MissionFail.SetActive(true);
        pause();
    }
    IEnumerator WaitForProgress()
    {
        yield return new WaitForSeconds(10000f);

    }
}
