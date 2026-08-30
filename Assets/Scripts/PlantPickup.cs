using UnityEngine;

public class PlantPickup : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    private Renderer[] renderers;
    [HideInInspector] public bool isHeld = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // Solo sigue al HoldPoint si está siendo sostenida
        if (isHeld && transform.parent != null)
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }
    }

    // Levantar la planta
    public void PickUp(Transform holdPoint)
    {
        if (isHeld) return; // No hacer nada si ya está en mano

        isHeld = true;

        rb.isKinematic = true; // Desactiva física
        col.enabled = false;   // Desactiva colisión

        // Se hace hijo del HoldPoint del Player
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        foreach (var r in renderers)
            r.enabled = true;
    }

    // Soltar la planta
    public void Drop()
    {
        if (!isHeld) return;

        isHeld = false;

        rb.isKinematic = false; // Reactiva física
        col.enabled = true;

        transform.SetParent(null); // Se suelta del HoldPoint
    }
}
