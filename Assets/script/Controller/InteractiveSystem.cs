using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveSystem : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
             float interactRang = 2f ;
            Collider[] coliderArray = Physics.OverlapSphere(transform.position, interactRang);
            foreach(Collider collider in coliderArray)
            {
              if(collider.TryGetComponent(out InteractableObj InteractableObj))
                {
                    InteractableObj.Interacting();
                }
            }
        }
       
    }
    public TurretInteractable GetTurretInteractable()
    {
        float interactRang = 10f;
        Collider[] coliderArray = Physics.OverlapSphere(transform.position, interactRang);
        foreach (Collider collider in coliderArray)
        {
            if (collider.TryGetComponent(out TurretInteractable turretInteractable))
            {
                Debug.Log("Too Close");
                return turretInteractable;
            }
            
        }
        return null;
    }
}
