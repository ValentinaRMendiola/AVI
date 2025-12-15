using UnityEngine;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && !pickedup)
        {
            crosshair1.SetActive(true);
            crosshair2.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (!interactable) return;

        if (Input.GetMouseButtonDown(0))
        {
            PickUp();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }

        if (pickedup && Input.GetMouseButtonDown(1))
        {
            Throw();
        }
    }

    void PickUp()
    {
        objTransform.parent = cameraTrans;

        objRigidbody.isKinematic = true;
        objRigidbody.useGravity = false;
        objRigidbody.velocity = Vector3.zero;
        objRigidbody.angularVelocity = Vector3.zero;

        pickedup = true;
    }

    void Drop()
    {
        objTransform.parent = null;

        objRigidbody.isKinematic = false;
        objRigidbody.useGravity = true;

        pickedup = false;
    }

    void Throw()
    {
        objTransform.parent = null;

        objRigidbody.isKinematic = false;
        objRigidbody.useGravity = true;

        objRigidbody.velocity = cameraTrans.forward * throwAmount;

        pickedup = false;
    }
}
