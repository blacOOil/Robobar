using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveSystem : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
             float interactRang = 10f ;
            Collider[] coliderArray = Physics.OverlapSphere(transform.position, interactRang);
            foreach(Collider collider in coliderArray)
            {
              if(collider.TryGetComponent(out InteractableObj InteractableObj))
                {
                    InteractableObj.Interacting();
                }
                TurretInteractable closestTurret = GetTurretInteractable();
                if (collider.TryGetComponent(out TurretInteractable turretInteractable))
                {
                    turretInteractable.Interacting();
                }

            }
        }
       
    }
    public TurretInteractable GetTurretInteractable()
    {
        List<TurretInteractable> turretInteractablesList = new List<TurretInteractable>();
        float interactRang = 10f;
        Collider[] coliderArray = Physics.OverlapSphere(transform.position, interactRang);
        foreach (Collider collider in coliderArray)
        {
            if (collider.TryGetComponent(out TurretInteractable turretInteractable))
            {
                turretInteractablesList.Add(turretInteractable);
            }
        }
        TurretInteractable cloestTurrentInteract = null;
       foreach(TurretInteractable turretInteractable1 in turretInteractablesList)
        {
            if(cloestTurrentInteract == null)
            {
                cloestTurrentInteract = turretInteractable1;
            }
            else
            {
                if(Vector3.Distance(transform.position, turretInteractable1.transform.position)<
                  Vector3.Distance(transform.position, cloestTurrentInteract.transform.position))
                {
                    cloestTurrentInteract = turretInteractable1;
               
                }
            }
        }
        return cloestTurrentInteract;
    }
}
