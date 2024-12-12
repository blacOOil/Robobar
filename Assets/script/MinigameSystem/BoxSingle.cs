using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxSingle : MonoBehaviour
{
    public Transform spawnPoint;
    public float CheckerRadius ;
    public List<GameObject> PlacementCanvas;
    public List<TMP_Text> PlacementText;
    public CrossingRoadManager crossingRoadManager;
    public float scoreResetDelay = 2f;
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            HandlePlayerInteraction(0);
        }
        else if (other.CompareTag("Player2"))
        {
            HandlePlayerInteraction(1);
        }
        else if (other.CompareTag("Player3"))
        {
            HandlePlayerInteraction(2);
        }
    }
    private void HandlePlayerInteraction(int playerIndex)
    {
            PlacementCanvas[playerIndex].SetActive(true);
            crossingRoadManager.PlayerScoreint[playerIndex]++;
            isPlayerScored = true;

            // Start the coroutine to reset the score flag
            StartCoroutine(ResetScoreFlag());
        
    }
    private IEnumerator ResetScoreFlag()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(scoreResetDelay);

        // Reset the score flag
        isPlayerScored = false;
    }


}
