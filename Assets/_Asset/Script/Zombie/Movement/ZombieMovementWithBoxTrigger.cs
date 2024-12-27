using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovementWithBoxTrigger : MonoBehaviour
{
    public event Action<bool> onWaitingStateChanged;
    public BoxCollider moveArea;
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private bool isWaiting;

    public bool IsWaiting
    {
        get { return isWaiting; }
        set
        {
            if (isWaiting != value)
            {
                isWaiting = value;
                onWaitingStateChanged?.Invoke(isWaiting);
            }
        }
    }    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        IsWaiting = false;
        MoveToRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaiting )
        {
            StartCoroutine(WaitAndMove());
        }
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomPoint = GetRandomPoinBox();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, Mathf.Max(moveArea.size.x, moveArea.size.y), NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private Vector3 GetRandomPoinBox()
    {
        Vector3 areaCenter = moveArea.transform.position + moveArea.center;
        Vector3 areaSize = moveArea.size;

        float randomX = UnityEngine.Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = UnityEngine.Random.Range(-areaSize.z / 2, areaSize.z / 2);

        Vector3 randomPoint = new Vector3(randomX,0, randomZ) + areaCenter;

        return randomPoint;

    }

    private IEnumerator WaitAndMove()
    {
        IsWaiting = true;
        yield return new WaitForSeconds(waitTime);
        MoveToRandomPosition();
        IsWaiting = false;
    }
}
