using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

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

    public void MoveToPlayer (Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void StopMoving()
    {
        agent.ResetPath();
    }

    public void ZombieDie()
    {
        agent.ResetPath();
        agent.speed = 0;
        agent.angularSpeed = 0;

    }
}
