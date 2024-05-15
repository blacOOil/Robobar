using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoint : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    
    private List<CheckpointSingle> CheckPointsList;
    public Transform CheckPoints;
    private int nextCheckpointIndex;
    private void Awake()
    {

        Transform checkPointsTransform = CheckPoints;

       CheckPointsList = new List<CheckpointSingle>();
        foreach (Transform CheckPointBlockTranform in checkPointsTransform)
        {
            CheckpointSingle checkpointSingle = CheckPointBlockTranform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);

            CheckPointsList.Add(checkpointSingle);
        }
        nextCheckpointIndex = 0;
    }
    public void Update()
    {
        CheckpointSingle correctCheckpointSingle = CheckPointsList[nextCheckpointIndex];
        correctCheckpointSingle.Show();

        
    }
    public void CarThroungCheckPoint(CheckpointSingle checkPointSingle)
    {
        if (CheckPointsList.IndexOf(checkPointSingle) == nextCheckpointIndex)
        {
            nextCheckpointIndex = (nextCheckpointIndex + 1) % CheckPointsList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
            CheckpointSingle correctCheckpointSingle = CheckPointsList[nextCheckpointIndex];
            correctCheckpointSingle.Hide();
            Debug.Log("Corret");
        }
        else
        {
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);

            
        }
    }
}
