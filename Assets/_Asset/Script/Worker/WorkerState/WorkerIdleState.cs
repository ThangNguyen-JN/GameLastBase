using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerIdleState : IWorkerState
{
    public void EnterState(WorkerManager worker)
    {
        Debug.Log("Entering Idle State");
    }

    public void UpdateState(WorkerManager worker)
    {
        if (worker.HasResourceInSafeZone())
        {
            Debug.Log($"HasResourceInSafeZone {worker.HasResourceInSafeZone()}");
            worker.ChangeState(new WorkerMoveToResourceState());
        }
    }

    public void ExitState(WorkerManager worker)
    {
        Debug.Log("Exiting Idle State");
    }
}
