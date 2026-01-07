using UnityEngine;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;

    [Header("Seed Settings")]
    public bool isSeed;
    public Collider seedPhysicsCollider; // EL COLLIDER PEQUEÑO (MESH)
    public bool isPlanted;                 // 
    public Collider pickupTriggerCollider;
    public Rigidbody objRigidbody;
    public float throwAmount;

    [Header("Seed Duplication")]
    public bool isSeedSource;        // SOLO el objeto fuente
    public int maxSeeds = 5;         // máximo permitido
    private int currentSeeds = 1;

    private void OnValidate()
    {
        if (!isSeed)
        {
            if (seedPhysicsCollider != null)
                seedPhysicsCollider.enabled = false;
            return;
        }

        if (seedPhysicsCollider != null)
            seedPhysicsCollider.enabled = true;
    }


    private void OnTriggerStay(Collider other)
    {
        if (isPlanted) return;

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
        if (isPlanted) return;

        // SI YA ESTÁ TOMADO permitir soltar
        if (pickedup)
        {
            if (Input.GetMouseButtonUp(0))
                Drop();

            if (Input.GetMouseButtonDown(1))
                Throw();

            return;
        }

        // SOLO PARA INTENTAR TOMAR
        if (!interactable) return;

        if (Input.GetMouseButtonDown(0))
            PickUp();
    }

    void PickUp()
    {
        // DUPLICAR SOLO SI ES FUENTE
        if (isSeed && isSeedSource)
        {
            if (currentSeeds < maxSeeds)
            {
                objPickup newSeed = DuplicateSeed();
                newSeed.PickUpDirect(cameraTrans);
            }
            return; // NUNCA se toma la fuente
        }
        PickUpDirect(cameraTrans);
    }

    void PickUpDirect(Transform parent)
    {
        // SOLO semillas usan su collider especial
        if (isSeed && seedPhysicsCollider != null)
            seedPhysicsCollider.enabled = false;

        if (pickupTriggerCollider != null)
            pickupTriggerCollider.enabled = false;

        objTransform.parent = parent;

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

        if (isSeed && seedPhysicsCollider != null)
            seedPhysicsCollider.enabled = true;

        if (pickupTriggerCollider != null)
            pickupTriggerCollider.enabled = true;

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

    objPickup DuplicateSeed()
    {
        GameObject newSeed = Instantiate(
            gameObject,
            transform.position + transform.forward * 0.15f,
            transform.rotation
        );

        objPickup seedScript = newSeed.GetComponent<objPickup>();
        Rigidbody rb = newSeed.GetComponent<Rigidbody>();

        seedScript.isSeedSource = false;

        seedScript.currentSeeds = currentSeeds + 1;
        currentSeeds++;

        seedScript.isPlanted = false;
        seedScript.pickedup = false;
        seedScript.interactable = false;

        rb.isKinematic = true;
        rb.useGravity = false;

        seedScript.seedPhysicsCollider.enabled = false;
        seedScript.pickupTriggerCollider.enabled = false;

        return seedScript;
    }

}
