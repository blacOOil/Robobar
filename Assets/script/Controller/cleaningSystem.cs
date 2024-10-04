using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaningSystem : MonoBehaviour
{
    public Transform Hand;
    public bool Ishandholding,Isbusy;
    public GameObject Objholding;
    public LayerMask TrashLayer;
    public float TrashCheckerRadius = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Objholding = null;
        Ishandholding = false;
        Isbusy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ishandholding && !IsTrashbinClose())
        {
            if (PlayerInput())
            {
                ReleaseTrash();
            }
        }
        else if(Ishandholding && IsTrashbinClose())
        {
            if (PlayerInput())
            {
                DestroyingTrash();
            }
        }
    }
    public void DestroyingTrash()
    {
        Objholding.GetComponent<Rigidbody>().isKinematic = false;
        Objholding.transform.SetParent(null);
        Objholding.GetComponent<Collider>().isTrigger = false;
        Ishandholding = false;
        Destroy(Objholding);
        Objholding = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashTag"))
        {
            if (Ishandholding == false)
            {
                TransformObjToHand(collision.gameObject);
            }
            else
            {

            }

        }

    }
    private void TransformObjToHand(GameObject Obj)
    {
        // Set the drink's position and parent to the hand
        Obj.transform.SetParent(Hand);
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        Objholding = Obj;
        Rigidbody rb = Obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        Collider collider = Obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        Ishandholding = true;
    }
    public void ReleaseTrash()
    {
        Objholding.GetComponent<Rigidbody>().isKinematic = false;
        Objholding.transform.SetParent(null);
        Objholding.GetComponent<Collider>().isTrigger = false;
        StartCoroutine(DropDelay());
    }
    IEnumerator DropDelay()
    {
        yield return new WaitForSeconds(1f);
        Ishandholding = false;
        Objholding = null;
    }
    public bool PlayerInput()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsTrashbinClose()
    {
        if (Ishandholding)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, TrashCheckerRadius, TrashLayer);
            foreach(var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("TrashBin"))
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
