using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public LayerMask plantLayer;
    public Transform cam;
    public Transform holdPoint;

    private PlantPickup heldPlant; // Planta que actualmente se sostiene

    void Update()
    {
        // Levantar planta
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldPlant == null)
            {
                // Raycast desde la cámara
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, pickupRange, plantLayer))
                {
                    PlantPickup plant = hit.collider.GetComponent<PlantPickup>();
                    if (plant != null && !plant.isHeld)
                    {
                        heldPlant = plant;
                        heldPlant.PickUp(holdPoint);
                    }
                }
            }
        }

        // Soltar planta
        if (Input.GetKeyUp(KeyCode.E) && heldPlant != null)
        {
            heldPlant.Drop();
            heldPlant = null;
        }
    }
}
