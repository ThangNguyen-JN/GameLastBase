using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseSystem : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private bool isChasing = false;

    public bool IsChasing()
    {
        return isChasing;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartChasing(Transform targetTrasform)
    {
        target = targetTrasform;
        isChasing = true;
        UpdateDestination();
    }

    public void StopChasing()
    {
        target = null;
        isChasing = false;
        agent.ResetPath();
    }

    private void Update()
    {
        if (isChasing && target != null)
        {
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        if(agent.isActiveAndEnabled && target != null)
        {
            agent.SetDestination(target.position);
        }
    }
    

}
