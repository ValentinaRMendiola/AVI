using UnityEngine;

public class PlantableObject : MonoBehaviour
{
    private objPickup pickup;
    private bool inPlantZone;

    private void Start()
    {
        pickup = GetComponent<objPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlantZone"))
        {
            inPlantZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlantZone"))
        {
            inPlantZone = false;
        }
    }

    private void Update()
    {
        // Solo se puede plantar si:
        // - está siendo tomada
        // - está en zona plantable
        if (pickup.pickedup && inPlantZone && Input.GetKeyDown(KeyCode.E))
        {
            Plant();
        }
    }

    void Plant()
    {
        // Soltar de la cámara
        transform.parent = null;

        // Fijar la planta
        Rigidbody rb = pickup.objRigidbody;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;

        // Ajustar al suelo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            transform.position = hit.point;
            transform.up = hit.normal;
        }

        pickup.pickedup = false;
    }
}
