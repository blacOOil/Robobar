using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxSingle : MonoBehaviour
{
    public Transform spawnPoint;
    public float CheckerRadius = 4f;
    public List<GameObject> PlacementCanvas;
    public List<TMP_Text> PlacementText;
    public CrossingRoadManager crossingRoadManager;

    private bool isPlayerScored; // To prevent repeated score increments
    // Start is called before the first frame update
    void Start()
    {
        foreach (var canvas in PlacementCanvas)
        {
            canvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update each text element with the respective score
        for (int i = 0; i < PlacementText.Count; i++)
        {
            PlacementText[i].text = crossingRoadManager.PlayerScoreint[i].ToString();
        }
    }
    public bool IsPlayerClose()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CheckerRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (gameObject.CompareTag("Box1") && hitCollider.CompareTag("Player1"))
            {
                return HandlePlayerInteraction(0);
            }
            if (gameObject.CompareTag("Box2") && hitCollider.CompareTag("Player2"))
            {
                return HandlePlayerInteraction(1);
            }
            if (gameObject.CompareTag("Box3") && hitCollider.CompareTag("Player3"))
            {
                return HandlePlayerInteraction(2);
            }
        }
        return false;
    }
    private bool HandlePlayerInteraction(int playerIndex)
    {
        if (!isPlayerScored)
        {
            PlacementCanvas[playerIndex].SetActive(true);
            crossingRoadManager.PlayerScoreint[playerIndex]++;
            isPlayerScored = true;
        }
        return true;
    }


}
