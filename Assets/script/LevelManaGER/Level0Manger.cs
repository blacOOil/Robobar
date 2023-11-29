using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level0Manger : MonoBehaviour
{
    public GameObject Player,EndScreen,MissionFail;
    [SerializeField] TextMeshProUGUI RemainingOBJ,Timetext;
    [SerializeField] public float remainingTime;
    private float Dropped = 3f;
    // Start is called before the first frame update
    void Start()
    {
        EndScreen.SetActive(false);
        MissionFail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RemainingOBJ.text = string.Format("{0}", Dropped);
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        Timetext.text = string.Format("{0:00}:{1:00}", minutes, second);

        if (Dropped < 2)
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
            Dropped--;
            RemainingOBJ.text = Dropped.ToString();
        }
        if (other.CompareTag("InstanceSpotlightTag"))
        {
            Fail();
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
