using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCam;

    public GameObject crosshairNormal;

    public GameObject crosshairInteract;

    [Header("Pickup")]

    public float interactDistance = 4;

    public float holdDistance = 2;

    public float pullForce = 250;

    private objPickup lookedObject;

    private objPickup heldObject;

    void Update()
    {
        DetectObject();

        HandleInput();
    }

    void FixedUpdate()
    {
        MoveHeldObject();
    }

    void DetectObject()
    {
        lookedObject = null;

        crosshairNormal.SetActive(true);

        crosshairInteract.SetActive(false);

        Ray ray = new Ray(
            playerCam.transform.position,
            playerCam.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactDistance))
        {
            objPickup pickup =
                hit.collider.GetComponentInParent<objPickup>();

            if (pickup != null &&
                !pickup.isPlanted)
            {
                lookedObject = pickup;

                crosshairNormal.SetActive(false);

                crosshairInteract.SetActive(true);
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryPickup();

            else
                DropHeld();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject != null)
                ThrowHeld();
        }
    }

    void TryPickup()
    {
        if (lookedObject == null)
            return;

        objPickup target = lookedObject;

        if (target.CanCreateSeed())
        {
            target = target.CreateSeedCopy();
        }

        heldObject = target;

        heldObject.PickUp();
    }

    void DropHeld()
    {
        heldObject.Drop();

        heldObject = null;
    }

    void ThrowHeld()
    {
        heldObject.Throw(
            playerCam.transform.forward
        );

        heldObject = null;
    }

    void MoveHeldObject()
    {
        if (heldObject == null)
            return;

        Vector3 targetPos =
            playerCam.transform.position +
            playerCam.transform.forward *
            holdDistance;

        Vector3 direction =
            targetPos -
            heldObject.transform.position;

        heldObject.objRigidbody.AddForce(
            direction * pullForce,
            ForceMode.Acceleration
        );
    }
}