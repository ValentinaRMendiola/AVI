using UnityEngine;

public class LoopMovement : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = (Time.time * speed) % distance;
        transform.position = startPos + Vector3.forward * offset;
    }
}
