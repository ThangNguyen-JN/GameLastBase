using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    public bool isDead = false;

    public void Update()
    {
        if (agent.velocity.magnitude > 0.1f && !agent.isStopped)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }

    public void MoveToPlayer(Vector3 targetPosition)
    {
        if (isDead) return;
        if (!gameObject.activeInHierarchy || agent == null) return;
        if (!agent.enabled)
        {
            Debug.LogError("NavMeshAgent is disabled!", gameObject);
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Zombie is not on NavMesh!", gameObject);
            return;
        }
        agent.SetDestination(targetPosition);
    }

    public void StopMoving()
    {
        if (isDead) return;
        if (!gameObject.activeInHierarchy || agent == null) return;
        if (!agent.enabled || !agent.isOnNavMesh) return;

        agent.isStopped = true;
        agent.ResetPath();
    }

    public void StopMovingWhenDead()
    {
        if (!gameObject.activeInHierarchy || agent == null) return;

        if (agent.enabled)
        {
            if (agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
            agent.enabled = false;
        }
    }

    public void ZombieDie()
    {
        if (isDead) return;

        isDead = true;
        StopMovingWhenDead();
    }
}
