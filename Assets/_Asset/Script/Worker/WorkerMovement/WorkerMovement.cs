using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject targetResource;
    public Animator anim;
    public GameObject targetStorage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void MoveToResource (GameObject resource)
    {
        if (resource != null)
        {
            targetResource = resource;
            agent.SetDestination(resource.transform.position);
        }
    }

    public void MoveToStorageHome(GameObject storageHome)
    {
        if (storageHome != null)
        {
            targetStorage = storageHome;
            agent.SetDestination(storageHome.transform.position);
        }
    }

    public bool HasReachedDestination()
    {
        if (agent.pathPending == true)
        {
            return false;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (targetResource != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetResource.transform.position);
            Gizmos.DrawSphere(targetResource.transform.position, 0.5f);
        }
    }
}
