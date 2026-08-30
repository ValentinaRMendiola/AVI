using UnityEngine;

public class objPickup : MonoBehaviour
{
    [HideInInspector]
    public bool pickedup;

    [Header("Seed Settings")]

    public bool isSeed;

    public bool isPlanted;

    public Collider seedPhysicsCollider;

    public Rigidbody objRigidbody;

    public float throwAmount = 8;

    [Header("Seed Duplication")]

    public bool isSeedSource;

    public int maxSeeds = 5;

    private int currentSeeds = 1;

    public void PickUp()
    {
        pickedup = true;

        objRigidbody.useGravity = false;

        objRigidbody.drag = 10f;
    }

    public void Drop()
    {
        pickedup = false;

        objRigidbody.useGravity = true;

        objRigidbody.drag = 0f;
    }

    public void Throw(Vector3 dir)
    {
        Drop();

        objRigidbody.velocity = dir * throwAmount;
    }

    public objPickup CreateSeedCopy()
    {
        GameObject clone = Instantiate(
            gameObject,
            transform.position,
            transform.rotation
        );

        objPickup seed = clone.GetComponent<objPickup>();

        seed.isSeedSource = false;

        currentSeeds++;

        seed.currentSeeds = currentSeeds;

        return seed;
    }

    public bool CanCreateSeed()
    {
        return isSeed &&
               isSeedSource &&
               currentSeeds < maxSeeds;
    }
}