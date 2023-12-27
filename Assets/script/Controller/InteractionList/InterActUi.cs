using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActUi : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObj;
    [SerializeField] private InteractiveSystem interactiveSystem;

    private void Update()
    {
        if (interactiveSystem.GetTurretInteractable() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        containerGameObj.SetActive(true);
    }
    private void Hide()
    {
        containerGameObj.SetActive(false);
    }
}
