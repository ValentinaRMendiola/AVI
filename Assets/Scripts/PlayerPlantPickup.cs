using UnityEngine;

public class PlayerPlantPickup : MonoBehaviour
{
    public Transform holdPoint;
    private PlantPickup targetPlant; // planta actualmente apuntada
    private PlantPickup heldPlant;

    void Update()
    {
        // PickUp
        if (Input.GetMouseButtonDown(0) && targetPlant != null && heldPlant == null)
        {
            heldPlant = targetPlant;
            heldPlant.PickUp(holdPoint);
        }

        // Drop
        if (Input.GetMouseButtonUp(0) && heldPlant != null)
        {
            heldPlant.Drop();
            heldPlant = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlantPickup plant = other.GetComponent<PlantPickup>();
        if (plant != null)
        {
            targetPlant = plant;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlantPickup plant = other.GetComponent<PlantPickup>();
        if (plant != null && targetPlant == plant)
        {
            targetPlant = null;
        }
    }
}
