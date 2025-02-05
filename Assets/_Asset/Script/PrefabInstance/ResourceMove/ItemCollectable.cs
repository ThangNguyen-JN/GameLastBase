using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemCollectable : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isMoving = false;
    private Transform target;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false; // Tắt NavMeshAgent
    }

    public void MoveToTarget(Transform newTarget)
    {
        target = newTarget;
        agent.enabled = true; // Bật NavMeshAgent
        agent.SetDestination(target.position); // Đặt mục tiêu di chuyển
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && agent.enabled && !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                CollectResource(); // Khi tới nơi thì nhặt tài nguyên
            }
        }
    }

    private void CollectResource()
    {
        Debug.Log("Tài nguyên đã đến nơi!");
        Destroy(gameObject); // Hủy vật phẩm sau khi đến nơi
    }
}
