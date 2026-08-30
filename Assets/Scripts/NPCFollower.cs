using UnityEngine;
using UnityEngine.AI;

public class NPCFollower : MonoBehaviour
{
    public Transform target;
    public float followDistance = 1.6f;
    public float repathDistance = 0.8f;

    private NavMeshAgent agent;
    private Animator animator;

    private bool isFollowing;
    private Vector3 lastTargetPos;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.stoppingDistance = followDistance;
    }

    void Update()
    {
        if (!isFollowing || target == null)
        {
            SetSpeed(0);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        // SOLO se mueve si está lejos
        if (distance > followDistance)
        {
            agent.isStopped = false;

            //Recalcular path SOLO si el target se movió lo suficiente
            if (Vector3.Distance(lastTargetPos, target.position) > repathDistance)
            {
                agent.SetDestination(target.position);
                lastTargetPos = target.position;
            }

            SetSpeed(agent.velocity.magnitude);
            RotateTowardsMovement();
        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
            SetSpeed(0);
        }
    }

    public void StartFollowing(Transform newTarget)
    {
        target = newTarget;
        isFollowing = true;
        agent.isStopped = false;
        lastTargetPos = Vector3.zero;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        target = null;

        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero; 
        SetSpeed(0);
    }

    private void SetSpeed(float speed)
    {
        // Usa desiredVelocity para animación más fluida
        float animSpeed = agent.desiredVelocity.magnitude;
        animator.SetFloat("Speed", animSpeed, 0.15f, Time.deltaTime);
    }


    private void RotateTowardsMovement()
    {
        if (agent.desiredVelocity.sqrMagnitude < 0.01f) return;

        Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity.normalized);
        rot.x = 0;
        rot.z = 0;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rot,
            8f * Time.deltaTime
        );
    }

}
