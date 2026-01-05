using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SeedSlot : MonoBehaviour
{
    [Header("Debug")]
    public bool occupied;

    private Collider slotTrigger;

    void Awake()
    {
        slotTrigger = GetComponent<Collider>();
        slotTrigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //  PRIMER FILTRO ABSOLUTO
        if (occupied) return;

        objPickup seed = other.GetComponentInParent<objPickup>();
        if (seed == null) return;

        //  solo semillas válidas
        if (!seed.isSeed) return;
        if (seed.isPlanted) return;
        if (seed.pickedup) return;

        //  SOLO el collider de físicas
        if (other != seed.seedPhysicsCollider) return;

        //  BLOQUEO INMEDIATO (ANTES DE TODO)
        occupied = true;
        slotTrigger.enabled = false;

        InsertSeed(seed);
    }

    void InsertSeed(objPickup seed)
    {
        Rigidbody rb = seed.objRigidbody;

        seed.transform.SetParent(transform);
        seed.transform.localPosition = Vector3.zero;
        seed.transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        rb.useGravity = false;

        seed.isPlanted = true;
        seed.interactable = false;
        seed.pickedup = false;

        seed.seedPhysicsCollider.enabled = false;
        seed.pickupTriggerCollider.enabled = false;
    }
}

